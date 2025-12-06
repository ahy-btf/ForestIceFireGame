using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace ClientWinformsExamples
{
    public partial class GameForm : Form
    {
        private TcpClient _client;
        private NetworkStream _stream;

        public GameForm()
        {
            InitializeComponent();
            _client = new TcpClient("127.0.0.1", 12345);
            _stream = _client.GetStream();
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            string message = "角色移动"; // 根据用户输入的控制指令发送
            byte[] data = Encoding.UTF8.GetBytes(message);
            _stream.Write(data, 0, data.Length);
            byte[] buffer = new byte[1024];
            int bytesRead = _stream.Read(buffer, 0, buffer.Length);
            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            messageLabel.Text = response;
        }

        // 其他UI事件（如角色按钮点击）可以添加在这里
    }
}
