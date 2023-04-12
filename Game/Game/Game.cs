namespace Game;

public class Game
{
    public void OnLeft(int startPositionLeft, int startPositionTop)
    {
        Console.Write(' ');
        Console.SetCursorPosition(startPositionLeft - 1, startPositionTop);
        Console.Write('@');
        Console.SetCursorPosition(startPositionLeft - 1, startPositionTop);
    }

    public void OnRight(int startPositionLeft, int startPositionTop)
    {
        Console.Write(' ');
        Console.SetCursorPosition(startPositionLeft + 1, startPositionTop);
        Console.Write('@');
        Console.SetCursorPosition(startPositionLeft + 1, startPositionTop);
    }

    public void OnUp(int startPositionLeft, int startPositionTop) 
    {
        Console.Write(' ');
        Console.SetCursorPosition(startPositionLeft, startPositionTop - 1);
        Console.Write('@');
        Console.SetCursorPosition(startPositionLeft, startPositionTop - 1);
    }

    public void OnDown(int startPositionLeft, int startPositionTop)
    {
        Console.Write(' ');
        Console.SetCursorPosition(startPositionLeft, startPositionTop + 1);
        Console.Write('@');
        Console.SetCursorPosition(startPositionLeft, startPositionTop + 1);
    }
}
