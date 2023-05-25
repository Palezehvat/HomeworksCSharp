namespace Game;

public delegate void ArrowHandler(int startPositionLeft, int startPositionTop, WorkWithConsole data, ref List<((int, int), char)> forTests);

/// <summary>
/// Event handler implementation class
/// </summary>
public static class EventLoop
{
    /// <summary>
    /// Event Handler
    /// </summary>
    /// <param name="left">Function for moving to the left</param>
    /// <param name="right">Function for moving to the right</param>
    /// <param name="up">Function for moving to the up</param>
    /// <param name="down">Function for moving to the down</param>
    /// <param name="fileWithMap">File with map</param>
    /// <param name="data">The selected type for testing or normal operation</param>
    /// <param name="forTests">A list intended for entering results for tests ONLY</param>
    /// <param name="listKeys">List of commands for tests ONLY</param>
    /// <exception cref="InvalidMapException">Incorrectly set map</exception>
    /// <exception cref="NullReferenceException">Checking that the read card line is not empty</exception>
    public static void Run(ArrowHandler left, ArrowHandler right, ArrowHandler up, ArrowHandler down, 
                    string fileWithMap, WorkWithConsole data, ref List<((int, int), char)> forTests, List<Char> listKeys = null)
    {
        int length = 0;
        int width = 0;
        var file = new StreamReader(fileWithMap);
        int sizeAtSymbols = 0;
        int sizeSpaces = 0;
        bool isFirst = true;
        while (!file.EndOfStream)
        {
            if (!isFirst)
            {
                if (width == 1 && sizeAtSymbols != length)
                {
                    throw new InvalidMapException();
                }
                else if (width != 1 && sizeAtSymbols != 2 || sizeSpaces != length - sizeAtSymbols)
                {
                    throw new InvalidMapException();
                }
            }
            var line = file.ReadLine();
            ++width;
            if (line == null)
            {
                throw new ArgumentException();
            }
            int size = line.Count(x => x == '@');
            if (length != 0 && length != line.Length)
            {
                throw new InvalidMapException();
            }
            length = line.Length;
            sizeAtSymbols = line.Count(x => x == '@');
            sizeSpaces = line.Count(x => x == ' ');
            isFirst = false;
            Console.WriteLine(line);
        }
        if (sizeAtSymbols != length)
        {
            throw new InvalidMapException();
        }

        if (width < 3 || length < 3)
        {
            throw new InvalidMapException();
        }
        data.SetCursorPosition(length / 2, width / 2, ref forTests);
        data.Print('@', ref forTests);
        int startPositionLeft = length / 2;
        int startPositionTop = width / 2;
        data.SetCursorPosition(startPositionLeft, startPositionTop, ref forTests);
        var checkType = new PrintInList();
        while (true)
        {
            if (data.GetType() == checkType.GetType() && listKeys.Count == 0)
            {
                return;
            }
            var key = data.Comparison(listKeys);
            switch (key)
            {
                case "left":
                    if (startPositionLeft != 1)
                    {
                        left(startPositionLeft, startPositionTop, data, ref forTests);
                        --startPositionLeft;
                    }
                    break;
                case "right":
                    if (startPositionLeft != length - 2)
                    {
                        right(startPositionLeft, startPositionTop, data, ref forTests);
                        ++startPositionLeft;
                    }
                    break;
                case "up":
                    if (startPositionTop != 1)
                    {
                        up(startPositionLeft, startPositionTop, data, ref forTests);
                        --startPositionTop;
                    }
                    break;
                case "down":
                    if (startPositionTop != width - 2) 
                    {
                        down(startPositionLeft, startPositionTop, data, ref forTests);
                        ++startPositionTop;
                    }
                    break;
            }
        }
    }
}
