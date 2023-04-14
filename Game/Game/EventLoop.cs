namespace Game;

public delegate void ArrowHandler(int startPositionLeft, int startPositionTop, WorkWithConsole data, ref List<((int, int), char)> forTests);

/// <summary>
/// Event handler implementation class
/// </summary>
public class EventLoop
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
    public void Run(ArrowHandler left, ArrowHandler right, ArrowHandler up, ArrowHandler down, 
                    string fileWithMap, WorkWithConsole data, ref List<((int, int), char)> forTests, List<Char> listKeys = null)
    {
        int sizeLong = 0;
        int sizeWidth = 0;
        var file = new StreamReader(fileWithMap);
        int sizeDogSymbols = 0;
        int sizeSpaces = 0;
        bool isFirst = true;
        while (!file.EndOfStream)
        {
            if (!isFirst)
            {
                if (sizeWidth == 1 && sizeDogSymbols != sizeLong)
                {
                    throw new InvalidMapException();
                }
                else if (sizeWidth != 1 && sizeDogSymbols != 2 || sizeSpaces != sizeLong - sizeDogSymbols)
                {
                    throw new InvalidMapException();
                }
            }
            var line = file.ReadLine();
            ++sizeWidth;
            int size = line.Count(x => x == '@');
            if (line == null)
            {
                throw new NullReferenceException();
            }
            if (sizeLong != 0 && sizeLong != line.Length)
            {
                throw new InvalidMapException();
            }
            sizeLong = line.Length;
            sizeDogSymbols = line.Count(x => x == '@');
            sizeSpaces = line.Count(x => x == ' ');
            isFirst = false;
            Console.WriteLine(line);
        }
        if (sizeDogSymbols != sizeLong)
        {
            throw new InvalidMapException();
        }

        if (sizeWidth < 3 || sizeLong < 3)
        {
            throw new InvalidMapException();
        }
        data.SetCursorPosition(sizeLong / 2, sizeWidth / 2, ref forTests);
        data.Print('@', ref forTests);
        int startPositionLeft = sizeLong / 2;
        int startPositionTop = sizeWidth / 2;
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
                    if (startPositionLeft != sizeLong - 2)
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
                    if (startPositionTop != sizeWidth - 2) 
                    {
                        down(startPositionLeft, startPositionTop, data, ref forTests);
                        ++startPositionTop;
                    }
                    break;
            }
        }
    }
}
