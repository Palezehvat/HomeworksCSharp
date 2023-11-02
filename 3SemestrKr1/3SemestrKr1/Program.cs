using System;
using _3SemestrKr1;

class Program
{
    public static async Task Main(string[] args)
    {

        if (args == null || args.Length > 2 || args.Length == 0)
        {
            throw new ArgumentException();
        }

        var port = 0;
        var correct = int.TryParse(args[0], out port);
        if (!correct)
        {
            throw new ArgumentException();
        }
        if (args.Length == 1)
        {
            var server = new ServerAndClient(8888);
            await server.StartServer();
            server.Talk();
        }
        else
        {
            var client = new ServerAndClient(args[1], port);
            client.Talk();
        }
    }
}