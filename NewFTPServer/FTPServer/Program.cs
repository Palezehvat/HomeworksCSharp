using SimpleFTP;

class Program
{
    static async Task Main()
    {
        Server server = new SimpleFTP.Server("C:/", 8888);

        Task serverTask = Task.Run(async () => await server.Start());

        Console.WriteLine("нажмите на любую клавишу для остановки серверка");
        Console.ReadKey();

        server.Stop();
    }
}