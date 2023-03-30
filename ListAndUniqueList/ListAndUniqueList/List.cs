﻿namespace ListAndUniqueList;

// Container for storing values 
public class List
{
    public ListElement? Head;
    public ListElement? Tail;

    // Adding element to list
    virtual public void AddElement(int value)
    {
        if (Head == null)
        {
            ListElement item = new ListElement(value, 0);
            Head = item;
            Tail = item;
            ++Head.SizeList;
        }
        else
        {
            ListElement item = new ListElement(value, Head.SizeList);
            Tail.Next = item;
            Tail = item;
            ++Head.SizeList;
        }
    }

    // Deleting element in list
    virtual public void RemoveElement(ref int item)
    {
        if (Head == null || Tail == null)
        {
            throw new NullPointerException();
        }
        if (Tail == Head)
        {
            item = Tail.Value;
            Tail = null;
            Head = null;
            return;
        }
        --Head.SizeList;
        ListElement walker = Head;
        while (walker.Next.Next != null)
        {
            walker = walker.Next;
        }
        item = walker.Next.Value;
        Tail = walker;
        walker.Next = null;
    }

    // Print all elements in list
    virtual public void PrintTheElements()
    {
        ListElement walker = Head;
        while (walker != null)
        {
            Console.Write(walker.Position);
            Console.Write(' ');
            Console.WriteLine(walker.Value);
            walker = walker.Next;
        }
    }

    // Change value by position
    virtual public void ChangeValueByPosition(int position, int newValue)
    {
        if (Head == null || Tail == null)
        {
            throw new NullPointerException();
        }
        ListElement? walker = Head;
        while (walker != null && walker.Position != position)
        {
            walker = walker.Next;
        }
        if (walker != null)
        {
            walker.Value = newValue;
        }
    }

    // Checks if the list is empty
    virtual public bool IsEmpty()
    {
        return Head == null;
    }

    // Returns the value by position
    virtual public int ReturnValueByPosition(int Position)
    {
        if (Head == null || Tail == null)
        {
            throw new NullPointerException();
        }
        var walker = Head;
        while (walker != null && walker.Position != Position)
        {
            walker = walker.Next;
        }
        if (walker == null)
        {
            throw new InvalidPositionException();
        }
        return walker.Value;
    }

    public class ListElement
    {
        public ListElement(int value, int position)
        {
            Value = value;
            Position = position;
        }
        public int Value { get; set; }
        public ListElement Next { get; set; }

        public int Position { get; set; }

        public int SizeList { get; set; }
    }
}
