namespace Game;

/// <summary>
/// Implements an interface for tests
/// </summary>
public class PrintInConsole:WorkWithConsole
{
    public override void Print(char symbol, ref List<((int, int), char)> forTests)
    {
        Console.Write(symbol);
    }

    public override void SetCursorPosition(int positionLeft, int PositionTop, ref List<((int, int), char)> forTests)
    {
        Console.SetCursorPosition(positionLeft, PositionTop);
    }

    public override string Comparison(List<char> listKeys = null)
    {
        var key = Console.ReadKey(true);
        switch (key.Key)
        {
            case ConsoleKey.LeftArrow:
                return "left";
            case ConsoleKey.RightArrow:
                return "right";
            case ConsoleKey.UpArrow:
                return "up";
            case ConsoleKey.DownArrow:
                return "down";
        }
        return "another";

    }
}