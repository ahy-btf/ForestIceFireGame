using System;
using System.Net.Sockets;
using System.Text;

namespace ClientConsoleExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            string serverAddress = "127.0.0.1";
            int port = 12345;

            // 连接到服务器
            TcpClient client = new TcpClient(serverAddress, port);
            NetworkStream stream = client.GetStream();

            Console.WriteLine("连接到服务器成功！");

            while (true)
            {
                // 输入信息
                Console.Write("请输入消息发送给服务器: ");
                string message = Console.ReadLine();
                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);

                // 接收服务器的回应
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("服务器回应: " + response);
            }
        }
    }
}
