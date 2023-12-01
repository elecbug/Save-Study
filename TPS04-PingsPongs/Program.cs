using System.Net;
using System.Net.Sockets;
using System.Text;

public class Program
{
    private static IPEndPoint IP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12356);

    public static void Main(string[] args)
    {
        string? msg = Console.ReadLine();

        switch (msg!.ToLower())
        {
            case "server":
                Server();
                break;
            case "client":
                Client();
                break;
        }
    }

    private static void Server()
    {
        byte[] buffer = new byte[1024];

        TcpListener server = new TcpListener(IP);

        server.Start();

        Socket s1 = server.AcceptSocket();
        Socket s2 = server.AcceptSocket();

        while (true)
        {
            s1.Receive(buffer);
            s2.Send(buffer);

            buffer = new byte[1024];

            s2.Receive(buffer);
            s1.Send(buffer);

            buffer = new byte[1024];
        }
    }

    private static void Client()
    {
        byte[] buffer = new byte[1024];

        TcpClient client = new TcpClient();

        client.Connect(IP);

        while (true)
        {
            string? msg = Console.ReadLine();

            client.GetStream().Write(ToBytes(msg!));

            buffer = new byte[1024];

            client.GetStream().Read(buffer);

            Console.WriteLine(ToString(buffer));
        }
    }

    private static byte[] ToBytes(string msg)
    {
        return Encoding.UTF8.GetBytes(msg);
    }

    private static string ToString(byte[] bytes)
    {
        return Encoding.UTF8.GetString(bytes);
    }
}

/// Mission
/// This connection is only can read if the other side write message.
/// How to apply threading and locking?