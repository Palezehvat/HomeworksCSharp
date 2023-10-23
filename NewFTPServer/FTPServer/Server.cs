using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleFTP;

/// <summary>
/// A class that implements an FTP server
/// </summary>
public class Server
{
    private static string? _locate;
    private static TcpListener? _listener;
    private static List<TcpClient>? _clients;
    private static CancellationTokenSource? _cancellationToken;
    private static List<Task>? _tasks;

    /// <summary>
    /// Class constructor
    /// </summary>
    /// <param name="locate">The absolute path of the server location</param>
    public Server(string locate, int port)
    {
        if (locate == null)
        {
            throw new ArgumentNullException();
        }
        _locate = locate;
        _listener = new TcpListener(IPAddress.Any, port);
        _cancellationToken = new CancellationTokenSource();
        _tasks = new List<Task>();
        _clients = new List<TcpClient>();
        Console.WriteLine($"Server started on port: {port}");
    }

    /// <summary>
    /// Starting the server
    /// </summary>
    public async Task Start()
    {
        if (_listener == null)
        {
            throw new ArgumentNullException(nameof(_listener));
        }

        if (_clients == null)
        {
            throw new ArgumentNullException(nameof(_clients));
        }

        if (_tasks == null)
        {
            throw new ArgumentNullException(nameof(_tasks));
        }

        if (_cancellationToken == null)
        {
            throw new ArgumentNullException(nameof(_cancellationToken));
        }

        _listener.Start();
        try
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                var client = await _listener.AcceptTcpClientAsync();
                _clients.Add(client);
                _tasks.Add(Listen(client));
            }

            Task.WaitAll(_tasks.ToArray());
            foreach (var client in _clients)
            {
                client.Dispose();
                client.Close();
            }

            _tasks.Clear();

            _listener.Stop();
        }
        finally
        {
            Stop();
        }
    }

    private static Task Listen(TcpClient client)
    {
        if (_cancellationToken == null)
        {
            throw new ArgumentNullException(nameof(_cancellationToken));
        }

        return Task.Run(async () =>
        {
            if (client == null)
            {
                throw new ArgumentNullException("client");
            }

            var stream = client.GetStream();
            var buffer = new byte[4096];

            if (!_cancellationToken.IsCancellationRequested)
            {
                var sizeCommand = await stream.ReadAsync(buffer, 0, buffer.Length);
                var command = new StringBuilder();
                command.Append(Encoding.UTF8.GetString(buffer, 0, sizeCommand));

                var stringCommand = command.ToString();

                if (command[0] == '1')
                {
                    List(stringCommand.TrimStart('1', ' ').TrimEnd('\n'), stream);
                }
                else
                {
                    Get(stringCommand.TrimStart('2', ' ').TrimEnd('\n'), stream);
                }
            }
        }
        );
    }

    /// <summary>
    /// Server shutdown
    /// </summary>
    public void Stop()
    {
        if (_listener != null && _cancellationToken != null)
        {
            _cancellationToken.Cancel();
        }
    }

    private static void Get(string directory, NetworkStream stream)
    {
        Task.Run(async () =>
        {
            if (_locate == null)
            {
                throw new NullReferenceException();
            }

            string combinePath = Path.Combine(_locate, directory);
            DirectoryInfo info = new DirectoryInfo(combinePath);
            var path = info.FullName;

            if (!File.Exists(path))
            {
                await stream.WriteAsync(Encoding.UTF8.GetBytes("-1"));
            }
            else
            {
                var text = await File.ReadAllTextAsync(path);
                text = text.Insert(0, text.Length.ToString() + " ");
                await stream.WriteAsync(Encoding.UTF8.GetBytes(text));
            }
        });
    }
    private static void List(string directory, NetworkStream stream)
    {
        Task.Run(async () =>
        {
            if (_locate == null)
            {
                throw new NullReferenceException();
            }
            
            string combinePath = Path.Combine(_locate, directory);
            DirectoryInfo info = new DirectoryInfo(combinePath);
            var path = info.FullName;

            if (!Directory.Exists(path))
            {
                await stream.WriteAsync(Encoding.UTF8.GetBytes("-1"));
            }
            else
            {
                var filesAndDirectories = new StringBuilder();
                var directories = Directory.GetDirectories(path);

                var sizeFilesAndDirectories = directories.Length;

                foreach (var directory in directories)
                {
                    filesAndDirectories.Append(" " + directory.Substring(directory.LastIndexOf('\\') + 1)  + " true");
                }

                var files = Directory.GetFiles(path);
                sizeFilesAndDirectories += files.Length;

                foreach (var file in files)
                {
                    filesAndDirectories.Append(" " + file.Substring(file.LastIndexOf('\\') + 1) + " false");
                }

                filesAndDirectories.Append('\n');
                filesAndDirectories.Insert(0, sizeFilesAndDirectories.ToString());

                await stream.WriteAsync(Encoding.UTF8.GetBytes(filesAndDirectories.ToString()));
            }
        });
    }
}