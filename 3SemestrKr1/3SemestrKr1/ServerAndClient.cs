using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;

namespace _3SemestrKr1;

public class ServerAndClient
{
    private static int _port;
    private static TcpListener? _listener;
    private static TcpClient? _client;
    private static string? _ipAddress;
    private static Socket? _socket;
    private static NetworkStream? stream;
    private static object locker = new object();

    public ServerAndClient(int port)
    {
        _port = port;
        _listener = new TcpListener(IPAddress.Any, port);
    }

    public async Task StartServer()
    {
        if (_listener == null)
        {
            throw new InvalidProgramException();
        }
        _client = await _listener.AcceptTcpClientAsync();
        _socket = _listener.AcceptSocket();
        stream = new NetworkStream(_socket);
    }

    public ServerAndClient(string ipAdress, int port)
    {
        _port = port;
        _ipAddress = ipAdress;
        _client = new TcpClient(ipAdress, port);
    }

    public void Talk()
    {
        if (_socket == null || stream == null)
        {
            throw new ArgumentNullException();
        }

        while (true)
        {
            var writer = new StreamWriter(stream);
            var reader = new StreamReader(stream);
            var data = reader.ReadToEnd();
            if (string.Compare(data, "exit") == 0)
            {
                _socket.Shutdown(SocketShutdown.Both);
                if (_client != null)
                {
                    _client.Close();
                }
                if (_listener!= null)
                {
                    _listener.Server.Close();
                }
                break;
            }
            Console.WriteLine(reader);
            var someText = Console.ReadLine();
            lock (locker)
            {
                writer.WriteLine(someText);
            }
        }
    }
}