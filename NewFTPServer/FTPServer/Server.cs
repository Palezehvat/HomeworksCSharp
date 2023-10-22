using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleFTP;

public class Server
{
    private static string? _locate;
    private static TcpListener? _listener;
    private static List<TcpClient>? _clients;
    private static CancellationTokenSource _cancellationToken;
    private static List<Task> _tasks;

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
                    List(stringCommand.TrimStart('1', ' '), stream);
                }
                else
                {
                    Get(stringCommand.TrimStart('2', ' '), stream);
                }
            }
        }
        );
    }

    public void Stop()
    {
        if (_listener != null)
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

            var path = Path.Combine(_locate, directory);//
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
            if (_locate == null)
            {
                throw new NullReferenceException();
            }

            var path = Path.GetFullPath(_locate, directory);
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