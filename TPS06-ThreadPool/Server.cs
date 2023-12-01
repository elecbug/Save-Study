using System.Net;
using System.Net.Sockets;

public class Server
{
    public IPEndPoint IP { get; private set; }
    public TcpListener Listener { get; private set; }
    public List<Socket> Sockets { get; private set; }
    public int Pools { get; private set; }

    public Server(IPEndPoint ip, int pools)
    {
        IP = ip;
        Listener = new TcpListener(ip);
        Sockets = new List<Socket>();
        Pools = pools;
    }

    public void Start()
    {
        Listener.Start();

        for (int i = 0; i < Pools; i++)
        {
            Thread thread = new Thread(RoofCare);
            thread.Start();
        }
    }

    private void RoofCare()
    {
        byte[] buffer = new byte[1024];

        while (true)
        {
            Socket socket = Listener.AcceptSocket();

            lock (Sockets)
            {
                Sockets.Add(socket);
            }

            while (true)
            {
                try
                {
                    socket.Receive(buffer);
                }
                catch (Exception ex)
                {
                    lock (Sockets)
                    {
                        Sockets.Remove(socket);

                        Console.WriteLine(ex.ToString());

                        break;
                    }
                }

                string msg = Program.ToString(buffer);

                Console.WriteLine(msg);

                lock (Sockets)
                {
                    foreach (Socket s in Sockets)
                    {
                        if (s == socket)
                        {
                            continue;
                        }

                        s.Send(buffer);
                    }
                }

                buffer = new byte[1024];
            }
        }
    }
}
