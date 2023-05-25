namespace ListAndUniqueList;

/// <summary>
/// A list for storing and interacting with int type elements
/// </summary>
public class List
{
    private ListElement? Head;
    private ListElement? Tail;

    /// <summary>
    /// Adding an item to the list
    /// </summary>
    virtual public void AddElement(int position, int value)
    {
        if (Head == null)
        {
            var item = new ListElement(value);
            Head = item;
            Tail = item;
            ++Head.SizeList;
        }
        else
        {
            ++Head.SizeList;
            ListElement item = new ListElement(value);
            if (Tail == null)
            {
                throw new NullReferenceException();
            }
            if (position == 0)
            {
                var temp = Head.Next;
                Head = item;
                Head.Next = temp;
                return;
            }
            if (position == Head.SizeList - 1)
            {
                Tail.Next = item;
                Tail = item;
                return;
            }
            var walker = Head;
            int counter = 0;

            while (walker != null && walker.Next != null && counter != position)
            {
                walker = walker.Next;
                counter++;
            }
            if (walker == null || walker.Next == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                var temp = walker.Next.Next;
                walker.Next = item;
                item.Next = temp;
            }
        }
    }

    /// <summary>
    /// Deletes an item at the end of the list
    /// </summary>
    /// <exception cref="NullPointerException">Throws an exception when the list is empty</exception>
    /// <returns>Element which was deleted</returns>
    virtual public int RemoveElement(int position)
    {
        if (Head == null || Tail == null)
        {
            throw new NullListException();
        }
        if (position > Head.SizeList)
        {
            throw new NullReferenceException();
        }
        if (Tail == Head)
        {
            var itemFromTail = Tail.Value;
            Tail = null;
            Head = null;
            return itemFromTail;
        }
        if (position == 0)
        {
            var itemFromHead = Head.Value;
            Head = Head.Next;
            return itemFromHead;
        }
        if (position == Head.SizeList)
        {
            var itemFromTail = Tail.Value;
            var walkerForTail = Head;
            while (walkerForTail != null && walkerForTail.Next != Tail)
            {
                walkerForTail = walkerForTail.Next;
            }
            if (walkerForTail == null)
            {
                throw new NullReferenceException();
            }
            Tail = walkerForTail;
            walkerForTail.Next = null;
        }

        int counter = 0;
        var walker = Head;
        while (walker != null && walker.Next.Next != null && counter != position - 1)
        {
            walker = walker.Next;
            counter++;
        }
        if (walker == null || walker.Next == null)
        {
            throw new NullReferenceException();
        }
        var item = walker.Next.Value;
        walker.Next = walker.Next.Next;
        --Head.SizeList;
        return item;
    }

    /// <summary>
    /// Prints list items
    /// </summary>
    virtual public void PrintTheElements()
    {
        ListElement walker = Head;
        int counter = 0;
        while (walker != null)
        {
            Console.Write(counter);
            Console.Write(' ');
            Console.WriteLine(walker.Value);
            walker = walker.Next;
            ++counter;
        }
    }

    /// <summary>
    /// Changes values by position
    /// </summary>
    /// <exception cref="NullPointerException">Throws an exception when the list is empty</exception>
    virtual public void ChangeValueByPosition(int position, int newValue)
    {
        if (Head == null || Tail == null)
        {
            throw new NullListException();
        }
        ListElement? walker = Head;
        int counter = 0;
        while (walker != null && counter != position)
        {
            walker = walker.Next;
            counter++;
        }
        if (walker == null)
        {
            throw new NullReferenceException();
        }
        walker.Value = newValue;
    }

    /// <summary>
    /// Checks if the list is empty
    /// </summary>
    virtual public bool IsEmpty() => Head == null;

    /// <summary>
    /// Returns the value by position
    /// </summary>
    /// <exception cref="NullPointerException">Throws an exception when the list is empty</exception>
    /// <exception cref="InvalidPositionException">Throws an excwption when there is no such position in the list</exception>
    virtual public int ReturnValueByPosition(int position)
    {
        if (Head == null || Tail == null)
        {
            throw new NullListException();
        }
        var walker = Head;
        int counter = 0;
        while (walker != null && counter != position)
        {
            walker = walker.Next;
            ++counter;
        }
        if (walker == null)
        {
            throw new InvalidPositionException();
        }
        return walker.Value;
    }

    /// <summary>
    /// Checks if there is an item in the list
    /// </summary>
    /// <returns>Returns true if present and false if absent</returns>
    public bool Contains(int value)
    {
        var walker = Head;
        while (walker != null)
        {
            if (walker.Value == value)
            {
                return true;
            }
            walker = walker.Next;
        }
        return false;
    }

    public class ListElement
    {
        public ListElement(int value)
        {
            Value = value;
        }
        public int Value { get; set; }

        public ListElement Next { get; set; }

        public int SizeList { get; set; }
    }
}
