using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerConsole
{
    internal class Program
    {
        private static async Task Main()
        {
            var gameState = new GameState();
            var listener = new TcpListener(IPAddress.Any, 12345);
            listener.Start();
            Console.WriteLine("服务器启动，等待客户端连接...");

            while (true)
            {
                var client = await listener.AcceptTcpClientAsync();
                Console.WriteLine("客户端已连接");
                _ = Task.Run(() => HandleClientAsync(client, gameState));
            }
        }

        private static async Task HandleClientAsync(TcpClient client, GameState state)
        {
            using var stream = client.GetStream();
            await SendAsync(stream, state.Serialize());

            var buffer = new byte[512];
            try
            {
                while (true)
                {
                    var bytesRead = await stream.ReadAsync(buffer);
                    if (bytesRead == 0)
                    {
                        break;
                    }

                    var request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    var response = state.ProcessCommand(request);
                    await SendAsync(stream, response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"客户端连接异常: {ex.Message}");
            }
        }

        private static Task SendAsync(NetworkStream stream, string message)
        {
            var data = Encoding.UTF8.GetBytes(message);
            return stream.WriteAsync(data, 0, data.Length);
        }
    }

    internal class GameState
    {
        private readonly object _lock = new();
        private Point _fire = new(80, 420);
        private Point _ice = new(150, 420);
        private readonly Rectangle _bounds = new(12, 12, 900, 560);
        private readonly Rectangle _exit = new(780, 80, 80, 80);
        private readonly List<Rectangle> _obstacles = new()
        {
            new Rectangle(200, 380, 140, 30),
            new Rectangle(420, 320, 120, 30),
            new Rectangle(610, 240, 160, 30),
            new Rectangle(340, 180, 90, 30)
        };
        private string _status = "请按下方按钮连接服务器";

        public string Serialize()
        {
            lock (_lock)
            {
                return $"STATE:{_fire.X},{_fire.Y};{_ice.X},{_ice.Y};{_status}";
            }
        }

        public string ProcessCommand(string command)
        {
            lock (_lock)
            {
                if (command.StartsWith("RESET", StringComparison.OrdinalIgnoreCase))
                {
                    Reset();
                    return Serialize();
                }

                if (command.StartsWith("INIT", StringComparison.OrdinalIgnoreCase))
                {
                    return Serialize();
                }

                if (!command.StartsWith("MOVE", StringComparison.OrdinalIgnoreCase))
                {
                    return "无法识别的命令";
                }

                var parts = command.Split(':');
                if (parts.Length != 4)
                {
                    return "命令格式错误";
                }

                var player = parts[1];
                if (!int.TryParse(parts[2], out var dx) || !int.TryParse(parts[3], out var dy))
                {
                    return "移动参数错误";
                }

                MovePlayer(player, dx, dy);
                return Serialize();
            }
        }

        private void MovePlayer(string player, int dx, int dy)
        {
            ref var target = ref player.Equals("Fire", StringComparison.OrdinalIgnoreCase) ? ref _fire : ref _ice;
            var next = new Point(target.X + dx, target.Y + dy);

            if (!IsInsideBounds(next) || HitsObstacle(next))
            {
                _status = "撞到了障碍，换个方向试试";
                return;
            }

            target = next;
            if (PlayerReachedExit())
            {
                _status = "双人已成功通关！";
            }
            else
            {
                _status = "继续寻找出口...";
            }
        }

        private void Reset()
        {
            _fire = new Point(80, 420);
            _ice = new Point(150, 420);
            _status = "重新开始，朝出口前进！";
        }

        private bool PlayerReachedExit()
        {
            return _exit.Contains(_fire) && _exit.Contains(_ice);
        }

        private bool IsInsideBounds(Point point)
        {
            return point.X >= _bounds.Left && point.X + 40 <= _bounds.Right &&
                   point.Y >= _bounds.Top && point.Y + 64 <= _bounds.Bottom;
        }

        private bool HitsObstacle(Point point)
        {
            var playerRect = new Rectangle(point, new Size(40, 64));
            foreach (var obstacle in _obstacles)
            {
                if (obstacle.IntersectsWith(playerRect))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
