namespace Routers;

/// <summary>
/// A list of arcs consisting of a pair of vertices and arcs between them
/// </summary>
public class ListArcs
{
    private ListElement? Head;
    private ListElement? Tail;

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
        if (Head == null)
        {
            throw new NullPointerException();
        }
        int size = Head.SizeList;
        for(int i = 0; i < size; ++i)
        {
            var walker = Head;
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
        if (Head == null)
        {
            throw new NullPointerException();
        }
        var walker = Head;
        if (walker.FromVertex == fromVertex && walker.ToVertex == toVertex)
        {
            Head = Head.Next;
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
            throw new NullPointerException();
        }
        var listArcs = graph.ReturnListArcs();
        var listVertexes = graph.ReturnListVertexes();
        listArcs.SortListArcs(false);
        var walker = listArcs.Head;
        int numberPlenty = 1;
        int extraPlenty = 0;
        while (walker != null)
        {
            if (walker.ToVertex != walker.FromVertex)
            {
                int returnedNumberPlentyFirstVertex = listVertexes.FromWichSet(walker.FromVertex);
                int returnedNumberPlentySecondVertex = listVertexes.FromWichSet(walker.ToVertex);

                if (returnedNumberPlentyFirstVertex != returnedNumberPlentySecondVertex)
                {
                    if (returnedNumberPlentyFirstVertex == 0 && returnedNumberPlentySecondVertex != 0)
                    {
                        listVertexes.ChangeOneVertexSet(walker.FromVertex, returnedNumberPlentySecondVertex);
                        --extraPlenty;
                    }
                    else if (returnedNumberPlentyFirstVertex != 0 && returnedNumberPlentySecondVertex == 0)
                    {
                        listVertexes.ChangeOneVertexSet(walker.ToVertex, returnedNumberPlentyFirstVertex);
                        --extraPlenty;
                    }
                    else 
                    {
                        listVertexes.ChangeNumbersSet(returnedNumberPlentyFirstVertex, returnedNumberPlentySecondVertex);
                        --extraPlenty;
                    }
                }
                else if (returnedNumberPlentyFirstVertex == 0)
                {
                    listVertexes.ChangeOneVertexSet(walker.FromVertex, numberPlenty);
                    listVertexes.ChangeOneVertexSet(walker.ToVertex, numberPlenty);
                    ++numberPlenty;
                    ++extraPlenty;
                }
                else
                {
                    DeleteArc(walker.FromVertex, walker.ToVertex);
                    --Head.SizeList;
                }
            }
            else
            {
                DeleteArc(walker.FromVertex, walker.ToVertex);
                --Head.SizeList;
            }
            walker = walker.Next;
        }
        SortListArcs(true);
        return !(walker == null && (extraPlenty != 1 && extraPlenty != 0));
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
        if (Head == null)
        {
            Head = item;
        }
        else
        {
            Tail.Next = item;
        }
        Tail = item;
        ++Head.SizeList;
    }

    /// <summary>
    /// Writing to the arc list file
    /// </summary>
    /// <param name="filePath">The path to the file</param>
    public void WirteToFile(string fileAfter)
    {
        StreamWriter file = new StreamWriter(fileAfter);
        var walker = Head;
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

        public int SizeList { get; set; }
    }
}
