namespace Game;

/// <summary>
/// Implements the interface for the console
/// </summary>
public class PrintInList:WorkWithConsole
{
    public override void Print(char symbol, ref List<((int, int), char)> forTests)
    {
        if (forTests == null)
        {
            throw new NullReferenceException();
        }
        forTests.Add(((-1, -1), symbol));
    }

    public override void SetCursorPosition(int positionLeft, int PositionTop, ref List<((int, int), char)> forTests)
    {
        if (forTests == null)
        {
            throw new NullReferenceException();
        }
        forTests.Add(((positionLeft, PositionTop), '\0'));
    }

    public override string Comparison(List<char> listKeys = null)
    {
        if (listKeys == null)
        {
            throw new NullReferenceException();
        }
        var key = listKeys.First();
        listKeys.RemoveAt(0);
        switch (key)
        {
            case (char)37:
                return "left";
            case (char)38:
                return "up";
            case (char)39:
                return "right";
            case (char)40:
                return "down";
        }
        return "another";

    }
}