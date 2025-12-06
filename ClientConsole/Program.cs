using System;
using System.Net.Sockets;
using System.Text;

namespace ClientConsole
{
    internal class Program
    {
        static void Main()
        {
            const string serverAddress = "127.0.0.1";
            const int port = 12345;

            var client = new TcpClient(serverAddress, port);
            using var stream = client.GetStream();
            Console.WriteLine("连接到服务器成功！使用 MOVE:Player:dx:dy 或 RESET 发送指令。");

            var buffer = new byte[512];
            var bytesRead = stream.Read(buffer, 0, buffer.Length);
            Console.WriteLine(Encoding.UTF8.GetString(buffer, 0, bytesRead));

            while (true)
            {
                var message = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(message))
                {
                    message = "INIT";
                }

                var data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);

                bytesRead = stream.Read(buffer, 0, buffer.Length);
                var response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine(response);
            }
        }
    }
}
