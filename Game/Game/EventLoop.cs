using System.IO;
using System.Text;

namespace Game;

public delegate void ArrowHandler(int startPositionLeft, int startPositionTop);

public class EventLoop
{
    public void Run(ArrowHandler left, ArrowHandler right, ArrowHandler up, ArrowHandler down, string fileWithMap)
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
        Console.SetCursorPosition(sizeLong / 2, sizeWidth / 2);
        Console.Write('@');
        int startPositionLeft = Console.CursorLeft - 1;
        int startPositionTop = Console.CursorTop;
        Console.SetCursorPosition(startPositionLeft, startPositionTop);
        while (true)
        {
            var key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    if (startPositionLeft != 1)
                    {
                        left(startPositionLeft, startPositionTop);
                        --startPositionLeft;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (startPositionLeft != sizeLong - 2)
                    {
                        right(startPositionLeft, startPositionTop);
                        ++startPositionLeft;
                    }
                    break;
                case ConsoleKey.UpArrow:
                    if (startPositionTop != 1)
                    {
                        up(startPositionLeft, startPositionTop);
                        --startPositionTop;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (startPositionTop != sizeWidth - 2) 
                    {
                        down(startPositionLeft, startPositionTop);
                        ++startPositionTop;
                    }
                    break;
            }
        }
    }
}
