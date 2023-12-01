using System.Net;
using System.Net.Sockets;

public class Client
{
    public IPEndPoint IP { get; private set; }
    public TcpClient TcpClient { get; private set; }
    public string Name { get; private set; }

    public Client(IPEndPoint ip, string name)
    {
        IP = ip;
        TcpClient = new TcpClient();
        Name = name;
    }

    public void Start()
    {
        TcpClient.Connect(IP);

        Thread reader = new Thread(Reader);
        reader.Start();

        Thread writer = new Thread(Writer);
        writer.Start();
    }

    private void Reader()
    {
        byte[] buffer = new byte[1024];

        while (true)
        {
            TcpClient.GetStream().Read(buffer);

            string msg = Program.ToString(buffer);

            Console.WriteLine(msg);

            buffer = new byte[1024];
        }
    }

    private void Writer()
    {
        byte[] buffer = new byte[1024];

        while (true)
        {
            string msg = "[" + Name + "] " + Console.ReadLine()!;

            buffer = Program.ToBytes(msg);

            TcpClient.GetStream().Write(buffer);

            buffer = new byte[1024];
        }
    }
}