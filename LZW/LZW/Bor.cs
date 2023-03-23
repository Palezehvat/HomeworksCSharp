namespace LZW;

// A container for storing strings, in the form of a suspended tree
public class Bor
{
    private BorElement root = new BorElement();

    // Adding an element
    public (bool, int) Add(Bor bor, char[] buffer, int from, int to)
    {
        if (root == null || buffer == null)
        {
            throw new InvalidOperationException();
        }
        var walker = root;
        int i = 0;
        int pointer = from;
        int toFlow = -1;
        bool isStringInBor = bor.Contains(buffer, from, to);
        while (i < to - from + 1)
        {
            int number = (int)buffer[pointer];
            if (!walker.Next.ContainsKey(number))
            {
                walker.Next.Add(number, new BorElement());
                ++walker.SizeDictionary;
            }
            if (!isStringInBor)
            {
                ++walker.Next[number].HowManyStringInDictionary;
            }
            toFlow = walker.Flow;
            walker = walker.Next[number];
            pointer++;
            i++;
        }
        if (walker.IsTerminal == false)
        {
            walker.Flow = root.HowManyStringInDictionary;
            root.HowManyStringInDictionary++;
        }

        return walker.IsTerminal == false ? (walker.IsTerminal = true && true, toFlow) : (false, toFlow);
    }

    public int ReturnFlowByCharArray(char[] buffer, int to, int from)
    {
        var walker = root;
        int i = 0;
        int pointer = from;
        while(i < to - from + 1)
        {
            int number = (int)buffer[pointer];
            if (!walker.Next.ContainsKey(number))
            {
                return -1;
            }
            walker = walker.Next[number];
            ++i;
        }
        return walker.Flow;
    }

    public int HowManyStringsInBor()
    {
        if (root == null)
        {
            return -1;
        }
        return root.HowManyStringInDictionary;
    }

    // Checks for the presence of a string
    public bool Contains(char[] buffer, int from, int to)
    {
        if (root == null)
        {
            return false;
        }
        var walker = root;
        int i = 0;
        int pointer = from;
        while (i < to - from + 1)
        {
            if (!walker.Next.ContainsKey(buffer[pointer]))
            {
                return false;
            }
            walker = walker.Next[buffer[pointer]];
            pointer++;
            ++i;
        }
        return true;
    }

    private class BorElement
    {
        public Dictionary<int, BorElement> Next { get; set; }
        public bool IsTerminal { get; set; }

        public int SizeDictionary { get; set; }

        public int HowManyStringInDictionary { get; set; }

        public int Flow { get; set; }

        public BorElement()
        {
            Next = new Dictionary<int, BorElement>();
            Flow = -1;
        }
    }
}
