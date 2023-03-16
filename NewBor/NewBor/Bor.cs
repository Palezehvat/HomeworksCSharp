namespace Bor;

// A container for storing strings, in the form of a suspended tree
public class Bor
{
    private const int alphabetSize = 65536;

    private BorElement root = new BorElement();

    // Adding an element
    public bool Add(string element)
    {
        if (root == null)
        {
            throw new InvalidOperationException();
        }
        if (element == null && !root.IsTerminal)
        {
            root.HowManyStringInDictionary++;
            root.IsTerminal = true;
            return true;
        }
        if (element != null)
        {
            root.HowManyStringInDictionary++;
        }
        if (element == null)
        {
            return false;
        }
        var walker = root;
        int i = 0;
        while (i < element.Length)
        {
            int number = (int)element[i];
            if (!walker.Next.ContainsKey(number))
            {
                walker.Next.Add(number, new BorElement());
                ++walker.SizeDictionary;
            }
            ++walker.Next[number].HowManyStringInDictionary;
            walker = walker.Next[number];
            i++;
        }
        return walker.IsTerminal == false ? walker.IsTerminal = true && true : false;
    }

    private bool RemoveHelp(BorElement walker, string element, int position, ref bool isDeleted)
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
            if (walker.Next[element[position]].HowManyStringInDictionary == 1)
            {
                walker.Next.Remove(element[position]);
                isDeleted = true;
                return true;
            }
            if (isDeleted == true)
            {
                --walker.Next[element[position]].SizeDictionary;
                isDeleted = false;
            }
            --walker.Next[element[position]].HowManyStringInDictionary;
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
            --root.HowManyStringInDictionary;
            return true;
        }
        return false;
    }

    // Counts the number of rows with the same prefix
    public int HowManyStartsWithPrefix(String prefix)
    {
        if (root == null)
        {
            if (prefix == null)
            {
                return 1;
            }
            return 0;
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
        return walker.HowManyStringInDictionary;
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

    private class BorElement
    {
        public Dictionary<int, BorElement> Next { get; set; }
        public bool IsTerminal { get; set; }

        public int SizeDictionary { get; set; }

        public int HowManyStringInDictionary { get; set; }

        public BorElement()
        {
            Next = new Dictionary<int, BorElement>();
        }
    }
}