using System.Net;
using System.Text;

public class Program
{
    private static IPEndPoint IP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12356);

    public static void Main(string[] args)
    {
        for (int i = 1; i <= 50; i += 5)
        {
            TpsTest(i);
            NormalTest(i);

            Console.WriteLine();
        }

        Task.Delay(-1);
    }

    public static void TpsTest(int pool)
    {
        DateTime start = DateTime.Now;

        TPSServer server = new TPSServer(IP, pool);
        server.Start();

        for (int i = 0; i < pool * 5; i++)
        {
            Client client = new Client(IP, i.ToString().PadLeft(3, '0'));
            client.Start();

            client.Stop();
        }

        server.Stop();

        Console.WriteLine("Thread pools server(" + pool + ") Time: " + (DateTime.Now - start).Microseconds + "ms");
    }

    public static void NormalTest(int pool)
    {
        DateTime start = DateTime.Now;

        NormalServer server = new NormalServer(IP);
        server.Start();

        for (int i = 0; i < pool * 5; i++)
        {
            Client client = new Client(IP, i.ToString().PadLeft(3, '0'));
            client.Start();

            client.Stop();
        }

        server.Stop();

        Console.WriteLine("Normal server(" + pool + ") Time: " + (DateTime.Now - start).Microseconds + "ms");
    }

    public static byte[] ToBytes(string msg)
    {
        return Encoding.UTF8.GetBytes(msg);
    }

    public static string ToString(byte[] bytes)
    {
        return Encoding.UTF8.GetString(bytes);
    }
}

/// Mission
/// Talking about the result to friends!
/// 
/// You are always best!
/// Can you take QnA?