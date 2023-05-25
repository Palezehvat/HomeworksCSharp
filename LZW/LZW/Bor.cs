namespace Bor;

/// <summary>
/// String Parsing Tree
/// </summary>
public class Bor
{
    private BorElement root = new();

    /// <summary>
    /// Adding an element to a Trie
    /// </summary>
    public (bool, int) Add(char[] buffer, int from = 0, int to = 0)
    {
        if (buffer == null)
        {
            throw new ArgumentNullException(nameof(buffer));
        }

        if (root == null)
        {
            throw new InvalidOperationException();
        }
        var walker = root;
        int i = 0;
        int pointer = from;
        int toFlow = -1;
        bool isStringInBor = Contains(buffer, from, to);
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
        if (!walker.IsTerminal)
        {
            walker.Flow = root.HowManyStringInDictionary;
            root.HowManyStringInDictionary++;
        }

        if (!walker.IsTerminal)
        {
            walker.IsTerminal = true;
            return (true, toFlow);
        }
        else
        {
            return (true, toFlow);
        }

    }

    /// <summary>
    /// Returns a stream by letter
    /// </summary>
    public int ReturnFlowByCharArray(char[] buffer, int to = 0, int from = 0)
    {
        if (buffer.Length == 0)
        {
            return -1;
        }
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

    /// <summary>
    /// Returns how many strings in a Trie
    /// </summary>
    public int HowManyStringsInBor()
    {
        if (root == null)
        {
            return -1;
        }
        return root.HowManyStringInDictionary;
    }

    /// <summary>
    /// Checks whether the string in the Trie contains
    /// </summary>
    public bool Contains(char[] buffer, int from = 0 , int to = 0)
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
