using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleFTP;

/// <summary>
/// A class that implements an FTP server
/// </summary>
public class Server
{
    private static string? locate;
    private static TcpListener? listener;
    private static List<TcpClient>? clients;
    private static CancellationTokenSource? cancellationToken;
    private static List<Task>? tasks;

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
        Server.locate = locate;
        listener = new(IPAddress.Any, port);
        cancellationToken = new();
        tasks = new();
        clients = new();
    }

    /// <summary>
    /// Starting the server
    /// </summary>
    public async Task Start()
    {
        if (listener == null || clients == null
            || tasks == null || cancellationToken == null)
        {
            throw new ArgumentNullException();
        }

        listener.Start();
        Console.WriteLine("Server started working");
        while (!cancellationToken.IsCancellationRequested)
        {
            Console.WriteLine("Waiting a client");
            var client = await listener.AcceptTcpClientAsync(cancellationToken.Token);
            clients.Add(client);
            tasks.Add(Listen(client));
        }

        Task.WaitAll(tasks.ToArray());
        foreach (var client in clients)
        {
            client.Close();
        }

        tasks.Clear();

        listener.Stop();
    }

    private static Task Listen(TcpClient client)
    {
        if (cancellationToken == null)
        {
            throw new ArgumentNullException(nameof(cancellationToken));
        }

        return Task.Run(async () =>
        {
            if (client == null)
            {
                throw new ArgumentNullException("client");
            }

            var stream = client.GetStream();

            if (!cancellationToken.IsCancellationRequested)
            {
                var reader = new StreamReader(stream, Encoding.UTF8);
                var stringCommand = await reader.ReadLineAsync();

                if (stringCommand == null)
                {
                    throw new InvalidOperationException();
                }

                if (stringCommand[0] == '1')
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

    /// <summary>
    /// Server shutdown
    /// </summary>
    public void Stop()
    {
        if (listener != null && cancellationToken != null)
        {
            cancellationToken.Cancel();
        }
    }

    private static void Get(string directory, NetworkStream stream)
    {
        Task.Run(async () =>
        {
            if (locate == null)
            {
                throw new NullReferenceException();
            }

            string combinePath = Path.Combine(locate, directory);
            DirectoryInfo info = new DirectoryInfo(combinePath);
            var path = info.FullName;

            if (!File.Exists(path))
            {
                await stream.WriteAsync(Encoding.UTF8.GetBytes("-1"));
            }
            else
            {
                var textBytes = File.ReadAllBytes(path);
                string text = Encoding.UTF8.GetString(textBytes);
                string newText = $"{textBytes.Length} {text}";
                await stream.WriteAsync(Encoding.UTF8.GetBytes(newText));
            }
        });
    }
    private static void List(string directory, NetworkStream stream)
    {
        Task.Run(async () =>
        {
            if (locate == null)
            {
                throw new NullReferenceException();
            }
            
            string combinePath = Path.Combine(locate, directory);
            DirectoryInfo info = new DirectoryInfo(combinePath);
            var path = info.FullName;

            if (!Directory.Exists(path))
            {
                await stream.WriteAsync(Encoding.UTF8.GetBytes("-1\n"));
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