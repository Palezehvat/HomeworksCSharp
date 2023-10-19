using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks.Sources;
using System.Transactions;

namespace SimpleFTP;

public class Server
{
    private static string? _locate;
    private static TcpListener? _listener;
    private static List<TcpClient>? _clients;
    private static CancellationToken _cancellationToken;
    private static List<Task> _tasks;

    public Server(string locate, int port)
    {
        if (locate == null)
        {
            throw new ArgumentNullException();
        }
        _locate = locate;
        _listener = new TcpListener(IPAddress.Any, port);
        _clients = new List<TcpClient>();
        _cancellationToken = new CancellationToken();
    }

    public static async Task<string> Start()
    {
        _listener.Start();
        try
        {
            while (true)
            {
                var client = await _listener.AcceptTcpClientAsync();
                _clients.Add(client);
                _tasks.Add(new Task(() => Listen(client)));
            }
        }
        finally
        { 
            _listener.Stop(); 
        }
    }

    private static async void Listen(TcpClient client)
    {

    }

    public void Stop()
    {
        if (_listener != null)
        {
            _listener.Stop();
        }
    }

    private static void Get(string directory, NetworkStream stream)
    {
        Task.Run(async () =>
        {
            var path = Path.Combine(_locate, directory);
            if (!File.Exists(path))
            {
                await stream.WriteAsync(Encoding.UTF8.GetBytes("-1"));
            }
            else
            {
                var text = await File.ReadAllTextAsync(path);
                await stream.WriteAsync(Encoding.UTF8.GetBytes(text));
            }
        });
    }
    private static void List(string directory, NetworkStream stream)
    {
        Task.Run(async () =>
        {
            var path = Path.Combine(_locate, directory);
            if (!File.Exists(path))
            {
                await stream.WriteAsync(Encoding.UTF8.GetBytes("-1"));
            }
            else
            {
                var filesAndDirectories = new StringBuilder();
                var directories = Directory.GetFiles(path);

                var sizeFilesAndDirectories = directories.Length;

                foreach (var directory in directories)
                {
                    filesAndDirectories.Append(" " + directory + " true");
                }

                var files = Directory.GetFiles(path);
                sizeFilesAndDirectories += files.Length;

                foreach (var file in files)
                {
                    filesAndDirectories.Append(" " + file + "false");
                }
                
                filesAndDirectories.Append('\n');
                filesAndDirectories.Insert(0, sizeFilesAndDirectories.ToString());

                await stream.WriteAsync(Encoding.UTF8.GetBytes(filesAndDirectories.ToString()));
            }
        });
    }

    /*
    private static void Get(NetworkStream stream, TcpClient client)
    {
        Task.Run(async () => 
        {
            var writer = new StreamWriter(stream) { AutoFlush = true };
            var allText = new StringBuilder();
            byte[] data = new byte[512];
            var socket = _listener.AcceptSocket();
            int sizeData = 0;
            do
            {
                sizeData = await stream.ReadAsync(data, _cancellationToken);
                allText.Append(Encoding.UTF8.GetString(data, 0, sizeData));
            } while (sizeData > 0);
        });
    }
    */

}