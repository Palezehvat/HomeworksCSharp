namespace Game;

/// <summary>
/// The interface for the game, namely for tests and normal startup
/// </summary>
interface GameInterface
{
    /// <summary>
    /// For a normal launch, it prints to the console for tests, it is included in a special list
    /// </summary>
    /// <param name="symbol">Symbol for printing</param>
    /// <param name="forTests">The list is necessary ONLY for tests</param>
    void Print(char symbol, ref List<((int, int), char)> forTests);

    /// <summary>
    /// For normal work, it puts the cursor in the console at the specified coordinates, for tests it fixes cursor changes in a special list
    /// </summary>
    /// <param name="positionLeft">X coordinate</param>
    /// <param name="PositionTop">Y coordinates</param>
    /// <param name="forTests">The list is for tests ONLY changes are being made here</param>
    void SetCursorPosition(int positionLeft, int PositionTop, ref List<((int, int), char)> forTests);

    /// <summary>
    /// Different comparison for tests and for a normal run
    /// </summary>
    /// <param name="listKeys">List of commands for tests only</param>
    /// <returns>Returns a string: left up right down</returns>
    string Comparison(List<char> listKeys = null);
}