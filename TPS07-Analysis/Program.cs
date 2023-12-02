using System.Net;
using System.Text;

public class Program
{
    private static IPEndPoint IP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12356);

    public static void Main(string[] args)
    {
        string? msg = Console.ReadLine();

        switch (msg!.ToLower())
        {
            case "s":
                TPSServer server = new TPSServer(IP, 2);
                server.Start();

                break;
            case "c":
                int rand = new Random().Next();

                Console.WriteLine("ID: " + rand);

                Client client = new Client(IP, rand.ToString());
                client.Start();

                break;
        }
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