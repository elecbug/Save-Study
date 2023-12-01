using System.Net;

public class Client
{
    public IPEndPoint IP { get; private set; }

    public Client(IPEndPoint ip)
    {
        IP = ip;
    }
}