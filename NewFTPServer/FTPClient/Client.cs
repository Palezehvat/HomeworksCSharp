using System.Net.Sockets;
using System.Text;

namespace SimpleFTP;

/// <summary>
/// FTP Client implementation class
/// </summary>
public class Client
{
    private static int _port;
    private static string? _hostname;

    /// <summary>
    /// Class constructor
    /// </summary>
    public Client(int port, string hostname)
    {
        _port = port;
        _hostname = hostname;
        Console.WriteLine("Client started!");
    }

    /// <summary>
    /// Downloading a file from the server
    /// </summary>
    /// <param name="filePath">The relative path of the file from the specified path on the server</param>
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

    /// <summary>
    /// Listing files in a directory on the server
    /// </summary>
    /// <param name="directoryPath">The relative path of the directory from the specified path on the server</param>
    public async Task<string> List(string directoryPath)
    {
        if (_hostname == null)
        {
            throw new ArgumentNullException();
        }

        var client = new TcpClient(_hostname, _port);
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