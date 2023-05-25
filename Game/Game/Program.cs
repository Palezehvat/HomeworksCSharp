namespace Game;

public class Program
{
    public static void Main()
    {
        char symbol = (char)38;
        Console.WriteLine("Input your string with file path");
        var filePath = Console.ReadLine();
        Console.Clear();
        List<((int, int), char)> list = new List<((int, int), char)>();
        try 
        {
            EventLoop.Run(
                new ArrowHandler(Game.OnLeft),
                new ArrowHandler(Game.OnRight),
                new ArrowHandler(Game.OnUp),
                new ArrowHandler(Game.OnDown),
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