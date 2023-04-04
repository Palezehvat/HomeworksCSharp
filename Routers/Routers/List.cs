namespace Routers;

// Container for storing values 
public class List
{
    private ListElement? Head;
    private ListElement? Tail;

    // Adding element to list
    public void AddElement(List list, int value, int bandwidthSize)
    {
        ListElement item = new ListElement();
        item.Value = new GraphElement(value, bandwidthSize);
        if (list.Head == null)
        {
            list.Head = item;
            list.Tail = item;
        }
        else
        {
            list.Tail.Next = item;
            list.Tail = item;
        }
    }

    public void AddAnArr(List list, int firstVertex, int secondVertex, int sizeArr)
    {
        var walkerFirstVertex = list.Head;
        while(walkerFirstVertex != null && walkerFirstVertex.Value.Vertex != firstVertex)
        {
            walkerFirstVertex = walkerFirstVertex.Next;
        }
        if (walkerFirstVertex == null)
        {
            throw new InvalidPositionException();
        }
        if (walkerFirstVertex.Value.Vertexes == null)
        {
            walkerFirstVertex.Value.Vertexes = new List();
        }
        AddElement(walkerFirstVertex.Value.Vertexes, secondVertex, sizeArr);
        var walkerSecondVertex = list.Head;
        while(walkerSecondVertex != null && walkerSecondVertex.Value.Vertex != secondVertex)
        {
            walkerSecondVertex = walkerSecondVertex.Next;
        }
        if (walkerSecondVertex == null)
        {
            throw new InvalidPositionException();
        }
        if (walkerSecondVertex.Value.Vertexes == null)
        {
            walkerSecondVertex.Value.Vertexes = new List();
        }
        AddElement(walkerSecondVertex.Value.Vertexes, firstVertex, sizeArr);
    }

    public void PrintGraphByList(List mainVertexes)
    {
        var walker = mainVertexes.Head;
        while (walker != null)
        {
            Console.Write(walker.Value.Vertex);
            Console.Write(": ");
            if (walker.Value.Vertexes != null)
            {
                var walkerAnotherVertexes = walker.Value.Vertexes.Head;
                while (walkerAnotherVertexes != null)
                {
                    Console.Write(walkerAnotherVertexes.Value.Vertex);
                    Console.Write("(");
                    Console.Write(walkerAnotherVertexes.Value.BandwidthSize);
                    Console.Write(") ");
                    walkerAnotherVertexes = walkerAnotherVertexes.Next;
                }
            }
            Console.WriteLine();
            walker = walker.Next;
        }
    }

    /*
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
    */
    
    private class ListElement
    {
        public GraphElement Value { get; set; }
        public ListElement Next { get; set; }
    }
}