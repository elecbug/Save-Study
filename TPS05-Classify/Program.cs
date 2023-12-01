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
            case "server":
                new Server(IP).Start();
                break;
            case "client":
                new Client(IP).Start();
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
/// Currently, client receive that from sending message by selves. 
/// How to fix? 
/// And, can not check message sender. 
/// Do you have coding idea to allocated sender ID?
/// Finally, server is died if only one client disconnected.
/// You can do try-catching?