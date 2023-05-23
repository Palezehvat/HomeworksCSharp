namespace Trie;

// A container for storing strings, in the form of a suspended tree
public class Trie
{
    private const int alphabetSize = 65536;

    private TrieElement root = new();

    // Adding an element
    public bool Add(string element)
    {
        if (element == null)
        {
            throw new ArgumentNullException();
        }
        root.NumberOfLinesInTheDictionary++;
        var walker = root;
        int i = 0;
        while (i < element.Length)
        {
            char number = element[i];
            if (!walker.Next.ContainsKey(number))
            {
                walker.Next.Add(number, new TrieElement());
                ++walker.DictionarySize;
            }
            ++walker.Next[number].NumberOfLinesInTheDictionary;
            walker = walker.Next[number];
            i++;
        }
        if (walker.IsTerminal == false)
        {
            walker.IsTerminal = true;
            return true;
        }
        return false;
    }

    private bool RemoveHelp(TrieElement walker, string element, int position, ref bool isDeleted)
    {
        if (position == element.Length)
        {
            if (walker.IsTerminal)
            {
                walker.IsTerminal = false;
                return true;
            }
            return false;
        }

        if (walker.Next.ContainsKey(element[position]))
        {
            bool isCorrect = RemoveHelp(walker.Next[element[position]], element, position + 1, ref isDeleted);
            if (!isCorrect)
            {
                return false;
            }
            if (walker.Next[element[position]].NumberOfLinesInTheDictionary == 1)
            {
                walker.Next.Remove(element[position]);
                isDeleted = true;
                return true;
            }
            if (isDeleted)
            {
                --walker.Next[element[position]].DictionarySize;
                isDeleted = false;
            }
            --walker.Next[element[position]].NumberOfLinesInTheDictionary;
        }
        else
        {
            return false;
        }
        return true;
    }

    // Deleting an element in the tree
    public bool Remove(string element)
    {
        if (root == null)
        {
            throw new InvalidOperationException();
        }
        if (element == null)
        {
            root.IsTerminal = false;
            return true;
        }
        var walker = root;
        bool flag = false;
        if (RemoveHelp(walker, element, 0, ref flag))
        {
            --root.NumberOfLinesInTheDictionary;
            return true;
        }
        return false;
    }

    // Counts the number of rows with the same prefix
    public int HowManyStartsWithPrefix(string prefix)
    {
        if (root == null)
        {
            return prefix == null ? 1 : 0;
        }
        var walker = root;
        int i = 0;
        while (i < prefix.Length)
        {
            if (!walker.Next.ContainsKey(prefix[i]))
            {
                return 0;
            }
            walker = walker.Next[prefix[i]];
            ++i;
        }
        return walker.NumberOfLinesInTheDictionary;
    }

    // Checks for the presence of a string
    public bool Contains(string element)
    {
        if (root == null)
        {
            return element == null;
        }
        var walker = root;
        int i = 0;
        while (i < element.Length)
        {
            if (!walker.Next.ContainsKey(element[i]))
            {
                return false;
            }
            walker = walker.Next[element[i]];
            ++i;
        }
        return true;
    }

    private class TrieElement
    {
        public Dictionary<char, TrieElement> Next { get; set; }
        public bool IsTerminal { get; set; }

        public int DictionarySize { get; set; }

        public int NumberOfLinesInTheDictionary { get; set; }

        public TrieElement()
        {
            Next = new Dictionary<char, TrieElement>();
        }
    }
}