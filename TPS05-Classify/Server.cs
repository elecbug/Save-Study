using System.Net;
using System.Net.Sockets;

public class Server
{
    public IPEndPoint IP { get; private set; }
    public TcpListener Listener { get; private set; }
    public List<Socket> Sockets { get; private set; }

    public Server(IPEndPoint ip)
    {
        IP = ip;
        Listener = new TcpListener(ip);
        Sockets = new List<Socket>();
    }

    public void Start()
    {
        Listener.Start();

        Thread thread = new Thread(RoofAccept);
        thread.Start();
    }

    private void RoofBroadcast(Socket socket)
    {
        while (true)
        {
            lock (socket)
            {
                socket.
            }
        }
    }

    private void RoofAccept()
    {
        while (true)
        {
            Socket socket = Listener.AcceptSocket();

            lock (Sockets)
            {
                Sockets.Add(socket);
            }

            Thread thread = new Thread(delegate () { RoofBroadcast(socket); });
            // Thread thread = new Thread(() => { RoofBroadcast(socket); });
        }
    }
}
