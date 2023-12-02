using System.Net;
using System.Net.Sockets;

public class Client
{
    public IPEndPoint IP { get; private set; }
    public TcpClient TcpClient { get; private set; }
    public string Name { get; private set; }
    public bool IsConnected { get; private set; }

    public Client(IPEndPoint ip, string name)
    {
        IP = ip;
        TcpClient = new TcpClient();
        Name = name;
        IsConnected = true;
    }

    public void Start()
    {
        TcpClient.Connect(IP);

        Thread reader = new Thread(Reader);
        reader.Start();

        Thread writer = new Thread(Writer);
        writer.Start();
    }

    public void Stop()
    {
        lock (this)
        {
            IsConnected = false;
        }

        TcpClient.GetStream().Close();
        TcpClient.Close();
    }

    private void Reader()
    {
        byte[] buffer = new byte[1024];

        while (IsConnected)
        {
            try
            {
                TcpClient.GetStream().Read(buffer);
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.ToString());

                break;
            }

            string msg = Program.ToString(buffer);

            Console.WriteLine(msg);

            buffer = new byte[1024];
        }
    }

    private void Writer()
    {
        byte[] buffer = new byte[1024];

        while (IsConnected)
        {
            string msg = "[" + Name + "] " + Console.ReadLine()!;

            buffer = Program.ToBytes(msg);

            try
            {
                TcpClient.GetStream().Write(buffer);
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.ToString());

                break;
            }

            buffer = new byte[1024];
        }
    }
}