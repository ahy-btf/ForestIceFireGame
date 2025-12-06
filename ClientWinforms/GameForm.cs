using System;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientWinforms
{
    public partial class GameForm : Form
    {
        private TcpClient? _client;
        private NetworkStream? _stream;
        private readonly object _streamLock = new();
        private Point _firePosition = new(80, 420);
        private Point _icePosition = new(150, 420);
        private Rectangle _exitArea = new(780, 80, 80, 80);

        public GameForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            LoadSprites();
            ApplyLayout();
        }

        private void ApplyLayout()
        {
            firePlayerPicture.Location = _firePosition;
            icePlayerPicture.Location = _icePosition;
            exitPicture.Location = _exitArea.Location;
            exitPicture.Size = _exitArea.Size;
            backgroundPicture.SendToBack();
        }

        private void LoadSprites()
        {
            backgroundPicture.Image = LoadResourceImage("游戏关卡.jpg", "游戏关卡1.png");
            firePlayerPicture.Image = LoadResourceImage("火娃静.png", "火娃右.png");
            icePlayerPicture.Image = LoadResourceImage("冰娃静.png", "冰娃右.png");
            exitPicture.Image = LoadResourceImage("出口.jpg", "成功.jpg");
        }

        private static Image? LoadResourceImage(params string[] fileNames)
        {
            foreach (var fileName in fileNames)
            {
                var candidate = Path.Combine(AppContext.BaseDirectory, "Resource", fileName);
                if (File.Exists(candidate))
                {
                    return Image.FromFile(candidate);
                }

                var devPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Resource", fileName);
                if (File.Exists(devPath))
                {
                    return Image.FromFile(devPath);
                }
            }

            return null;
        }

        private async Task EnsureConnectedAsync()
        {
            if (_client != null && _client.Connected)
            {
                return;
            }

            try
            {
                _client = new TcpClient();
                await _client.ConnectAsync("127.0.0.1", 12345);
                _stream = _client.GetStream();
                statusLabel.Text = "已连接，按键开始移动";
                await RequestStateAsync("INIT");
            }
            catch (Exception ex)
            {
                statusLabel.Text = $"连接失败: {ex.Message}";
            }
        }

        private async Task RequestStateAsync(string command)
        {
            if (_stream == null)
            {
                statusLabel.Text = "尚未连接服务器";
                return;
            }

            var data = Encoding.UTF8.GetBytes(command);
            try
            {
                lock (_streamLock)
                {
                    _stream.Write(data, 0, data.Length);
                }

                var buffer = new byte[512];
                int bytesRead = await _stream.ReadAsync(buffer);
                var response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                UpdateStateFromResponse(response);
            }
            catch (Exception ex)
            {
                statusLabel.Text = $"通信异常: {ex.Message}";
            }
        }

        private void UpdateStateFromResponse(string response)
        {
            if (!response.StartsWith("STATE:", StringComparison.OrdinalIgnoreCase))
            {
                statusLabel.Text = response;
                return;
            }

            var payload = response[6..];
            var parts = payload.Split(';');
            if (parts.Length < 3)
            {
                statusLabel.Text = "收到的状态格式不正确";
                return;
            }

            if (TryParsePoint(parts[0], out var fire))
            {
                _firePosition = fire;
                firePlayerPicture.Location = _firePosition;
            }

            if (TryParsePoint(parts[1], out var ice))
            {
                _icePosition = ice;
                icePlayerPicture.Location = _icePosition;
            }

            statusLabel.Text = parts[2];

            if (parts[2].Contains("通关"))
            {
                MessageBox.Show("恭喜，两人成功走出森林！", "胜利", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private static bool TryParsePoint(string value, out Point point)
        {
            point = Point.Empty;
            var coords = value.Split(',');
            if (coords.Length != 2)
            {
                return false;
            }

            if (int.TryParse(coords[0], out var x) && int.TryParse(coords[1], out var y))
            {
                point = new Point(x, y);
                return true;
            }

            return false;
        }

        private async void GameForm_KeyDown(object? sender, KeyEventArgs e)
        {
            var (player, dx, dy) = e.KeyCode switch
            {
                Keys.Left => ("Fire", -10, 0),
                Keys.Right => ("Fire", 10, 0),
                Keys.Up => ("Fire", 0, -10),
                Keys.Down => ("Fire", 0, 10),
                Keys.A => ("Ice", -10, 0),
                Keys.D => ("Ice", 10, 0),
                Keys.W => ("Ice", 0, -10),
                Keys.S => ("Ice", 0, 10),
                _ => (null, 0, 0)
            };

            if (player == null)
            {
                return;
            }

            await EnsureConnectedAsync();
            if (_client?.Connected == true)
            {
                await RequestStateAsync($"MOVE:{player}:{dx}:{dy}");
            }
        }

        private async void connectButton_Click(object? sender, EventArgs e)
        {
            await EnsureConnectedAsync();
        }

        private async void resetButton_Click(object? sender, EventArgs e)
        {
            await EnsureConnectedAsync();
            await RequestStateAsync("RESET");
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            _stream?.Dispose();
            _client?.Dispose();
        }
    }
}
