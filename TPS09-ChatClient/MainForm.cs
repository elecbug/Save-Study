using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TPS09_ChatClient
{
    public partial class MainForm : Form
    {
        const string CONNECT = "connect:";
        const string SEND = "send:";
        const string REQCONNECT = "req-connect:";
        const string REQSEND = "req-send:";

        private TcpClient client = new TcpClient();

        public MainForm()
        {
            InitializeComponent();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            client.Connect(IPEndPoint.Parse("127.0.0.1:5000"));

            client.GetStream().Write(Encoding.UTF8.GetBytes(CONNECT + NameBox.Text + ":"));

            Thread read = new Thread(() =>
            {
                while (true)
                {
                    byte[] buffer = new byte[1024];

                    client.GetStream().Read(buffer);

                    string msg = Encoding.UTF8.GetString(buffer).Replace("\0", "");

                    lock (OutputTextBox)
                    {
                        if (msg.StartsWith(REQCONNECT))
                        {
                            OutputTextBox.Invoke(() =>
                            { 
                                OutputTextBox.Text += "[" + msg.Split(':')[1] + "] ´ÔÀÌ Âü¿©ÇÏ¼Ì½À´Ï´Ù.\n"; 
                            });
                        }
                        else if (msg.StartsWith(REQSEND))
                        {
                            OutputTextBox.Invoke(() => 
                            {
                                OutputTextBox.Text += "[" + msg.Split(':')[1] + "]: " + msg.Split(':')[2] + "\n";
                            });
                            InputTextBox.Invoke(() =>
                            {
                                InputTextBox.Text = "";
                            });
                        }
                    }
                    buffer = new byte[1024];

                }
            });
            read.Start();
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            client.GetStream().Write(Encoding.UTF8.GetBytes(SEND + NameBox.Text + ":" + InputTextBox.Text));
        }
    }
}