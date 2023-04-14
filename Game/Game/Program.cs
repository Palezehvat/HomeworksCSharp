namespace Game;

public class Program
{
    public static void Main()
    {
        var game = new Game();
        var eventLoop = new EventLoop();
        Console.WriteLine("Input your string with file path");
        var filePath = Console.ReadLine();
        Console.Clear();
        List<((int, int), char)> list = new List<((int, int), char)>();
        try 
        {
            eventLoop.Run(
                new ArrowHandler(game.OnLeft),
                new ArrowHandler(game.OnRight),
                new ArrowHandler(game.OnUp),
                new ArrowHandler(game.OnDown),
                filePath,
                new PrintInConsole(),
                ref list
            );
        }
        catch (InvalidMapException)
        {
            Console.WriteLine("Incorrect map");
        }
        catch (NullReferenceException)
        {
            Console.WriteLine("Problems with file or reading lines in file");
        }
    }
}