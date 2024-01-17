using System.Net;
using System.Net.Sockets;
using System.Text;

public class Program
{
    private static List<Socket> sockets = new List<Socket>();

    public static void Main(string[] args)
    {
        TcpListener listener = new TcpListener(IPEndPoint.Parse("127.0.0.1:5000"));
        listener.Start();

        Thread accept = new Thread(() =>
        {
            while (true)
            {
                Socket socket = listener.AcceptSocket();
                
                lock (sockets)
                {
                    sockets.Add(socket);

                    Thread service = new Thread(() => { ServiceRoof(socket); });
                    service.Start();
                }
            }
        });
        accept.Start();
    }

    private static void ServiceRoof(Socket socket)
    {
        while (true)
        {
            byte[] buffer = new byte[1024];
            socket.Receive(buffer);

            const string CONNECT = "connect:";
            const string SEND = "send:"; 
            const string REQCONNECT = "req-connect:";
            const string REQSEND = "req-send:";

            string msg = Encoding.UTF8.GetString(buffer).Replace("\0", "");
            //connect:name:value

            Console.WriteLine(msg);

            if (msg.StartsWith(CONNECT))
            {
                lock (sockets)
                {
                    foreach (var s in sockets)
                    {
                        buffer = Encoding.UTF8.GetBytes(REQCONNECT + msg.Split(':')[1] + ":");
                        s.Send(buffer);

                        Console.WriteLine("send");
                    }
                }
            }
            else if (msg.StartsWith(SEND))
            {
                lock (sockets)
                {
                    foreach (var s in sockets)
                    {
                        buffer = Encoding.UTF8.GetBytes(REQSEND + msg.Split(':')[1] + ":" + msg.Split(':')[2]);
                        s.Send(buffer);
                    }
                }
            }
            else
            {

            }
        }
    }
}