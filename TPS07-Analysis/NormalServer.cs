using System.Net;
using System.Net.Sockets;

public class NormalServer
{
    public IPEndPoint IP { get; private set; }
    public TcpListener Listener { get; private set; }
    public List<Socket> Sockets { get; private set; }
    public bool IsConnected { get; private set; }

    public NormalServer(IPEndPoint ip)
    {
        IP = ip;
        Listener = new TcpListener(ip);
        Sockets = new List<Socket>();
        IsConnected = true;
    }

    public void Start()
    {
        Listener.Start();

        Thread thread = new Thread(RoofAccept);
        thread.Start();
    }

    public void Stop()
    {
        lock (this)
        {
            IsConnected = false;
        }

        Listener.Stop();
    }

    private void RoofBroadcast(Socket socket)
    {
        byte[] buffer = new byte[1024];

        while (IsConnected)
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

                    //Debug.WriteLine(ex.ToString());

                    break;
                }
            }

            //string msg = Program.ToString(buffer);

            //Console.WriteLine(msg);

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

    private void RoofAccept()
    {
        while (IsConnected)
        {
            Socket socket;

            try
            {
                socket = Listener.AcceptSocket();
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.ToString());

                break;
            }

            lock (Sockets)
            {
                Sockets.Add(socket);
            }

            Thread thread = new Thread(delegate () { RoofBroadcast(socket); });
            // Thread thread = new Thread(() => { RoofBroadcast(socket); });

            thread.Start();
        }
    }
}