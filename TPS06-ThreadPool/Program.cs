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
                new Server(IP, 3).Start();
                break;
            case "client":
                Console.Write("Name: ");
                new Client(IP, Console.ReadLine()!).Start();
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
/// What is happend if number of client over then the thread pools count?
/// You have two choices.
/// 1. Over clients are disconneted
/// 2. Thread pools size upgrade
/// In now, the mission is upgrade thread pools size over then over then over then... by 2^N but, that's size is maiximum 64.
/// And, how to stop server and client?
/// Finally, catch all exceptions that you can think about.