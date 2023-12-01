﻿using System.Net;
using System.Net.Sockets;
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
                Server();
                break;
            case "client":
                Client();
                break;
        }
    }

    private static void Server()
    {
        // 0
        byte[] buffer = new byte[1024];
        // 1
        TcpListener server = new TcpListener(IP);
        // 2
        server.Start();
        // 4
        Socket client = server.AcceptSocket();
        // 6
        client.Receive(buffer);
        // 7
        Console.WriteLine(ToString(buffer));
        // 8
        buffer = ToBytes("pong");
        // 9
        client.Send(buffer);
        // 13
        server.Stop();
    }

    private static void Client()
    {
        // 0
        byte[] buffer = new byte[1024];
        // 1
        TcpClient client = new TcpClient();
        // 3
        client.Connect(IP);
        // 5
        client.GetStream().Write(ToBytes("ping"));
        // 10
        client.GetStream().Read(buffer);
        // 11
        Console.WriteLine(ToString(buffer));
        // 12
        client.Close();
    }

    private static byte[] ToBytes(string msg)
    {
        return Encoding.UTF8.GetBytes(msg);
    }

    private static string ToString(byte[] bytes)
    {
        return Encoding.UTF8.GetString(bytes);
    }
}

/// Mission
/// The server send Console.WriteLine() message to client on behalf of the "ping".
/// And, the received client send Console.WriteLine() message to server on behalf of the "pong".