namespace RoutersByGraph;

/// <summary>
/// A list of arcs consisting of a pair of vertices and arcs between them
/// </summary>
public class ListEdges
{
    private ListElement? head;
    private ListElement? tail;

    private bool SortByVertexOrArcs(bool sortByVertex, ListElement firstListElement, ListElement secondListElement)
    {
        return sortByVertex ? firstListElement.FromVertex > secondListElement.FromVertex : firstListElement.SizeWay < secondListElement.SizeWay;
    }

    /// <summary>
    /// Sorting by the list bubble method
    /// </summary>
    /// <param name="sortByVertex">The parameter for selecting a comparison in the subsequent sorting is true if by vertices and false if by arcs</param>
    /// <exception cref="NullPointerException">Throws an exception if the list is empty</exception>
    public void SortListArcs(bool sortByVertex)
    {
        if (head == null)
        {
            throw new NullGraphOrGraphComponentsException();
        }
        int size = head.SizeListArcs;
        for(int i = 0; i < size; ++i)
        {
            var walker = head;
            for(int j = 0; j < size; ++j) 
            {
                if (walker.Next != null && SortByVertexOrArcs(sortByVertex, walker, walker.Next))
                {
                    var item = new ListElement(walker.FromVertex, walker.ToVertex, walker.SizeWay);
                    walker.SizeWay = walker.Next.SizeWay;
                    walker.FromVertex = walker.Next.FromVertex;
                    walker.ToVertex = walker.Next.ToVertex;
                    walker.Next.SizeWay = item.SizeWay;
                    walker.Next.ToVertex = item.ToVertex;
                    walker.Next.FromVertex = item.FromVertex;
                }
                walker = walker.Next;
            }
        }
    }

    /// <summary>
    /// Removes arcs from the list
    /// </summary>
    /// <param name="fromVertex">The vertex from which the arc goes</param>
    /// <param name="toVertex">The vertex to which the arc goes</param>
    /// <exception cref="NullPointerException">Throws an exception if the list is empty</exception>
    private void DeleteArc(int fromVertex, int toVertex)
    {
        if (head == null)
        {
            throw new NullGraphOrGraphComponentsException();
        }
        var walker = head;
        if (walker.FromVertex == fromVertex && walker.ToVertex == toVertex)
        {
            head = head.Next;
            return;
        }
        while (walker.Next != null)
        {
            if (walker.Next.FromVertex == fromVertex && walker.Next.ToVertex == toVertex)
            {
                walker.Next = walker.Next.Next;
                return;
            }
            walker = walker.Next;
        }
    }

    /// <summary>
    /// Kraskal's algorithm for a minimal spanning tree
    /// </summary>
    /// <param name="graph">The graph in which everything is stored</param>
    /// <returns>Returns false if the tree is not connected</returns>
    /// <exception cref="NullPointerException">Throws an exception if the graph and its components are empty</exception>
    public bool KraskalAlgorithm(Graph graph)
    {
        if (graph == null || graph.IsEmpty())
        {
            throw new NullGraphOrGraphComponentsException();
        }
        var listArcs = graph.ReturnListArcs();
        var listVertexes = graph.ReturnListVertexes();
        if (listArcs == null || listVertexes == null || head == null)
        {
            throw new NullReferenceException();
        }
        listArcs.SortListArcs(false);
        var walker = listArcs.head;
        int set = 1;
        int anotherSet = 0;
        while (walker != null)
        {
            if (walker.ToVertex != walker.FromVertex)
            {
                int returnedNumberPlentyFirstVertex = listVertexes.SearchForASuitableSet(walker.FromVertex);
                int returnedNumberPlentySecondVertex = listVertexes.SearchForASuitableSet(walker.ToVertex);

                if (returnedNumberPlentyFirstVertex != returnedNumberPlentySecondVertex)
                {
                    if (returnedNumberPlentyFirstVertex == 0 && returnedNumberPlentySecondVertex != 0)
                    {
                        listVertexes.ChangeOneVertexSet(walker.FromVertex, returnedNumberPlentySecondVertex);
                        --anotherSet;
                    }
                    else if (returnedNumberPlentyFirstVertex != 0 && returnedNumberPlentySecondVertex == 0)
                    {
                        listVertexes.ChangeOneVertexSet(walker.ToVertex, returnedNumberPlentyFirstVertex);
                        --anotherSet;
                    }
                    else 
                    {
                        listVertexes.ChangeNumbersSet(returnedNumberPlentyFirstVertex, returnedNumberPlentySecondVertex);
                        --anotherSet;
                    }
                }
                else if (returnedNumberPlentyFirstVertex == 0)
                {
                    listVertexes.ChangeOneVertexSet(walker.FromVertex, set);
                    listVertexes.ChangeOneVertexSet(walker.ToVertex, set);
                    ++set;
                    ++anotherSet;
                }
                else
                {
                    DeleteArc(walker.FromVertex, walker.ToVertex);
                    --head.SizeListArcs;
                }
            }
            else
            {
                DeleteArc(walker.FromVertex, walker.ToVertex);
                --head.SizeListArcs;
            }
            walker = walker.Next;
        }
        SortListArcs(true);
        return !(walker == null && anotherSet != 1 && anotherSet != 0);
    }

    /// <summary>
    /// Adds a new item to the list
    /// </summary>
    /// <param name="fromVertex">The vertex from which the arc originates</param>
    /// <param name="toVertex">The vertex that the arc enters</param>
    /// <param name="sizeWay">Arc Size</param>
    public void AddElement(int fromVertex, int toVertex, int sizeWay)
    {
        var item = new ListElement(fromVertex, toVertex, sizeWay);
        if (head == null)
        {
            head = item;
        }
        else
        {
            tail.Next = item;
        }
        tail = item;
        ++head.SizeListArcs;
    }

    /// <summary>
    /// Writing to the arc list file
    /// </summary>
    /// <param name="filePath">The path to the file</param>
    public void WirteToFile(string fileAfter)
    {
        var file = new StreamWriter(fileAfter);
        var walker = head;
        int previousMainVertex = 0;
        bool isFirst = true;
        while (walker != null)
        {
            if (walker.FromVertex != previousMainVertex)
            {
                if (!isFirst)
                {
                    file.WriteLine();
                }
                file.Write(walker.FromVertex);
                file.Write(": ");
                previousMainVertex = walker.FromVertex;
                isFirst = false;
            }
            else
            {
                file.Write(", ");
            }
            file.Write(walker.ToVertex);
            file.Write(" (");
            file.Write(walker.SizeWay);
            file.Write(")");
            walker = walker.Next;
        }
        file.Close();
    }

    private class ListElement
    {
        public ListElement(int fromVertex, int toVertex, int sizeWay)
        {
            FromVertex = fromVertex;
            ToVertex = toVertex;
            SizeWay = sizeWay;
        }

        public int FromVertex { get; set; }

        public int ToVertex { get; set; }
        public ListElement? Next { get; set; }

        public int SizeWay { get; set; }

        public int SizeListArcs { get; set; }
    }
}
