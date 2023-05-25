namespace Game;

/// <summary>
/// A class that implements actions in the game
/// </summary>
public static class Game
{
    /// <summary>
    /// Move to the Left
    /// </summary>
    /// <param name="startPositionLeft">X coordinate</param>
    /// <param name="startPositionTop">Y coordinate</param>
    /// <param name="data">Selected type: testing or normal launch</param>
    /// <param name="forTests">The list of entering results for tests ONLY</param>
    public static void OnLeft(int startPositionLeft, int startPositionTop, WorkWithConsole data, ref List<((int, int), char)> forTests)
    {
        data.Print(' ', ref forTests);
        data.SetCursorPosition(startPositionLeft - 1, startPositionTop, ref forTests);
        data.Print('@', ref forTests);
        data.SetCursorPosition(startPositionLeft - 1, startPositionTop, ref forTests);
    }

    /// <summary>
    /// Move to the Right
    /// </summary>
    /// <param name="startPositionLeft">X coordinate</param>
    /// <param name="startPositionTop">Y coordinate</param>
    /// <param name="data">Selected type: testing or normal launch</param>
    /// <param name="forTests">The list of entering results for tests ONLY</param>
    public  static void OnRight(int startPositionLeft, int startPositionTop, WorkWithConsole data, ref List<((int, int), char)> forTests)
    {
        data.Print(' ', ref forTests);
        data.SetCursorPosition(startPositionLeft + 1, startPositionTop, ref forTests);
        data.Print('@', ref forTests);
        data.SetCursorPosition(startPositionLeft + 1, startPositionTop, ref forTests);
    }

    /// <summary>
    /// Move to the Up
    /// </summary>
    /// <param name="startPositionLeft">X coordinate</param>
    /// <param name="startPositionTop">Y coordinate</param>
    /// <param name="data">Selected type: testing or normal launch</param>
    /// <param name="forTests">The list of entering results for tests ONLY</param>
    public static void OnUp(int startPositionLeft, int startPositionTop, WorkWithConsole data, ref List<((int, int), char)> forTests) 
    {
        data.Print(' ', ref forTests);
        data.SetCursorPosition(startPositionLeft, startPositionTop - 1, ref forTests);
        data.Print('@', ref forTests);
        data.SetCursorPosition(startPositionLeft, startPositionTop - 1, ref forTests);
    }

    /// <summary>
    /// Move to the Down
    /// </summary>
    /// <param name="startPositionLeft">X coordinate</param>
    /// <param name="startPositionTop">Y coordinate</param>
    /// <param name="data">Selected type: testing or normal launch</param>
    /// <param name="forTests">The list of entering results for tests ONLY</param>
    public static void OnDown(int startPositionLeft, int startPositionTop, WorkWithConsole data, ref List<((int, int), char)> forTests)
    {
        data.Print(' ', ref forTests);
        data.SetCursorPosition(startPositionLeft, startPositionTop + 1, ref forTests);
        data.Print('@', ref forTests);
        data.SetCursorPosition(startPositionLeft, startPositionTop + 1, ref forTests);
    }
}
