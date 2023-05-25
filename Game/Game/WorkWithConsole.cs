namespace Game;

/// <summary>
/// Ancestor of two implementation classes one for tests the other for use
/// </summary>
abstract public class WorkWithConsole : GameInterface
{
    virtual public void Print(char symbol, ref List<((int, int), char)> forTests) { }

    virtual public void SetCursorPosition(int positionLeft, int PositionTop,ref List<((int, int), char)> forTests) { }

    virtual public string Comparison(List<char> listKeys = null) { return null; }
}