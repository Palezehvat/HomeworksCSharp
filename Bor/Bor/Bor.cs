using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Bor;

// A container for storing strings, in the form of a suspended tree
public class BorClass
{
    const int sizeAlphabet = 65536;

    private BorElement? root = new BorElement();

    // Adding an element
    public bool Add(string element)
    {
        if (root == null)
        {
            throw new Exception();
        }
        if (element == null && !root.isTerminal)
        {
            root.howManyStringInDictionary++;
            root.isTerminal = true;
            return true;
        }
        if (element != null)
        {
            root.howManyStringInDictionary++;
        }
        if (element == null)
        {
            return false;
        }
        var walker = root;
        int i = 0;
        while (i < element.Length)
        {
            int number = element[i];
            if (!walker.next.ContainsKey(number))
            {
                walker.next.Add(number, new BorElement());
                ++walker.sizeDictionary;
            }
            ++walker.next[number].howManyStringInDictionary;
            walker = walker.next[number];
            i++;
        }
        return walker.isTerminal == false ? walker.isTerminal = true && true : false;
    }

    private bool RemoveHelp(BorElement walker, string element, int position, ref bool isDeleted)
    {
        if (position == element.Length)
        {
            if (walker.isTerminal == true)
            {
                walker.isTerminal = false;
                return true;
            }
            return false;
        }

        if (walker.next.ContainsKey(element[position]))
        {
            bool isCorrect = RemoveHelp(walker.next[element[position]], element, position + 1, ref isDeleted);
            if (!isCorrect)
            {
                return false;
            }
            if (walker.next[element[position]].howManyStringInDictionary == 1)
            {
                walker.next.Remove(element[position]);
                isDeleted = true;
                return true;
            }
            if (isDeleted == true)
            {
                --walker.next[element[position]].sizeDictionary;
                isDeleted = false;
            }
            --walker.next[element[position]].howManyStringInDictionary;
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
            throw new Exception();
        }
        if (element == null)
        {
            root.isTerminal = false;
            return true;
        }
        var walker = root;
        bool flag = false;
        if (RemoveHelp(walker, element, 0, ref flag))
        {
            --root.howManyStringInDictionary;
            return true;
        }
        return false;
    }

    private int HowManyStartsWithPrefixHelp(BorElement walker, String prefix, int position)
    {
        if (position == prefix.Length)
        {
            return walker.howManyStringInDictionary;
        }

        if (walker.next.ContainsKey(prefix[position]))
        {
            return HowManyStartsWithPrefixHelp(walker.next[prefix[position]], prefix, position + 1);
        }
        else
        {
            return 0;
        }
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
        return HowManyStartsWithPrefixHelp(walker, prefix, 0);
    }

    private bool ContainsHelp(string element, BorElement walker, int position)
    {
        if (position == element.Length)
        {
            return true;
        }
        if (walker.next.ContainsKey(element[position]))
        {
            return ContainsHelp(element, walker.next[element[position]], position + 1);
        }
        return false;
    }

    // Checks for the presence of a string
    public bool Contains(string element)
    {
        if (root == null)
        {
            if (element == null)
            {
                return true;
            }
            return false;
        }
        var walker = root;
        return ContainsHelp(element, walker, 0);
    }

    private class BorElement
    {
        public Dictionary<int, BorElement> next { get; set; }
        public bool isTerminal { get; set; }

        public int sizeDictionary { get; set; }

        public int howManyStringInDictionary { get; set; }

        public BorElement()
        {
            next = new Dictionary<int, BorElement>();
            isTerminal = false;
            sizeDictionary = 0;
            howManyStringInDictionary = 0;
        }
    }
}