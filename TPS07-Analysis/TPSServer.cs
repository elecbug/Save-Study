using System.Net;
using System.Net.Sockets;

public class TPSServer
{
    public IPEndPoint IP { get; private set; }
    public TcpListener Listener { get; private set; }
    public List<Socket> Sockets { get; private set; }
    public int Pools { get; private set; }
    public bool IsConnected { get; private set; }

    public TPSServer(IPEndPoint ip, int pools)
    {
        IP = ip;
        Listener = new TcpListener(ip);
        Sockets = new List<Socket>();
        Pools = pools;
        IsConnected = true;
    }

    public void Start()
    {
        Listener.Start();

        for (int i = 0; i < Pools; i++)
        {
            int tNum = i;
            Thread thread = new Thread(()=> { RoofCare(tNum); });
            thread.Start();
        }
    }

    public void Stop()
    {
        lock (this)
        {
            IsConnected = false;
        }

        Listener.Stop();
    }

    private void RoofCare(int tNum)
    {
        byte[] buffer = new byte[1024];

        while (IsConnected)
        {
            Socket socket;

            lock (Listener)
            {
                socket = Listener.AcceptSocket();

                Console.WriteLine("Thread #" + tNum + " is connected");
            }

            lock (Sockets)
            {
                Sockets.Add(socket);
            }

            while (true)
            {
                try 
                { 
                    socket.Receive(buffer);

                    string msg = Program.ToString(buffer);

                    Console.WriteLine(msg);
                }
                catch (Exception ex)
                {
                    lock (Sockets)
                    {
                        Sockets.Remove(socket);

                        //Debug.WriteLine(ex.ToString());

                        Console.WriteLine("Thread #" + tNum + " is disconnected");

                        break;
                    }
                }

                lock (Sockets)
                {
                    foreach (Socket s in Sockets)
                    {
                        if (s == socket)
                        {
                            continue;
                        }

                        try
                        {
                            s.Send(buffer);
                        }
                        catch (Exception ex)
                        {
                            //Debug.WriteLine(ex.ToString());
                        }
                    }
                }

                buffer = new byte[1024];
            }
        }
    }
}
