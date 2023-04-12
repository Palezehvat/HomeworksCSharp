namespace Game;

public class Program
{
    public static void Main()
    {
        var game = new Game();
        var eventLoop = new EventLoop();
        var filePath = @"C:\Users\User\source\repos\HomeworksCSharp\Game\Map.txt";
        eventLoop.Run(
            new ArrowHandler(game.OnLeft),
            new ArrowHandler(game.OnRight),
            new ArrowHandler(game.OnUp),
            new ArrowHandler(game.OnDown),
            filePath
        );
    }
}