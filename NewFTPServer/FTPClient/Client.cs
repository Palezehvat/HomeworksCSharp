using System.Net.Sockets;
using System.Text;

namespace SimpleFTP;

public class Client
{
    private static int _port;
    private static string? _hostname;

    public Client(int port, string hostname)
    {
        _port = port;
        _hostname = hostname;
        Console.WriteLine("Client started!");
    }

    public async Task<string> Get(string filePath)
    {
        if (_hostname == null)
        {
            throw new ArgumentNullException();
        }

        var client = new TcpClient(_hostname, _port);


        var stream = client.GetStream();

        await stream.WriteAsync(Encoding.UTF8.GetBytes("2 " + filePath + "\n"));
        await stream.FlushAsync();

        return await GetResultFromStream(stream, "Get");
    }

    public async Task<string> List(string directoryPath)
    {
        if (_hostname == null)
        {
            throw new ArgumentNullException();
        }

        var client = new TcpClient(directoryPath, _port);
        var stream = client.GetStream();

        await stream.WriteAsync(Encoding.UTF8.GetBytes("1 " + directoryPath + "\n"));
        await stream.FlushAsync();

        return await GetResultFromStream(stream, "List");
    }

    private static async Task<string> GetResultFromStream(NetworkStream stream, string method)
    {
        var buffer = new byte[4096];

        var sizeResult = await stream.ReadAsync(buffer, 0, buffer.Length);
        var result = new StringBuilder();
        result.Append(Encoding.UTF8.GetString(buffer, 0, sizeResult));

        return result.ToString();
    }
}