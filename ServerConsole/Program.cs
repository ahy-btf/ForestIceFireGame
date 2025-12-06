using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerConsoleExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            // 设置服务器端口
            int port = 12345;
            TcpListener server = new TcpListener(IPAddress.Any, port);
            server.Start();
            Console.WriteLine("服务器启动，等待客户端连接...");

            while (true)
            {
                // 等待客户端连接
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("客户端连接成功");

                // 处理客户端请求
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("客户端发送: " + message);

                    // 向客户端发送消息
                    byte[] response = Encoding.UTF8.GetBytes("服务器回应: " + message);
                    stream.Write(response, 0, response.Length);
                }
            }
        }
    }
}
