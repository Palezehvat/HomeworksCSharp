using System.Runtime.InteropServices;

namespace Routers;

public class ListArcs
{
    private ListElement? Head;
    private ListElement? Tail;

    private bool SortByVertexOrArcs(bool sortByVertex, ListElement firstListElement, ListElement secondListElement)
    {
        return sortByVertex ? firstListElement.ToVertex > secondListElement.ToVertex : firstListElement.SizeWay < secondListElement.SizeWay;
    }


    // Sort List Arcs
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

    public bool KraskalAlgorithm(Graph graph)
    {
        if (graph == null || graph.IsEmpty())
        {
            throw new NullPointerException();
        }
        var listArcs = graph.ReturnListArcs();
        var listVertexes = graph.ReturnListVertexes();
        listArcs.SortListArcs(false);
        int sizeGraph = listArcs.Head.SizeList;
        var walker = listArcs.Head;
        int numberPlenty = 1;
        int i = 1;
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
                    }
                    else if (returnedNumberPlentyFirstVertex != 0 && returnedNumberPlentySecondVertex == 0)
                    {
                        listVertexes.ChangeOneVertexSet(walker.ToVertex, returnedNumberPlentyFirstVertex);
                    }
                    else 
                    {
                        listVertexes.ChangeNumbersSet(returnedNumberPlentyFirstVertex, returnedNumberPlentySecondVertex);
                    }
                    ++i;
                }
                else if (returnedNumberPlentyFirstVertex == 0)
                {
                    listVertexes.ChangeOneVertexSet(walker.FromVertex, numberPlenty);
                    listVertexes.ChangeOneVertexSet(walker.ToVertex, numberPlenty);
                    ++numberPlenty;
                    i += 2;
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
        return !(walker == null && i <= sizeGraph);
    }

    // Adding element to list
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

    public void WirteToFile(string filePath)
    {
        filePath = filePath + ".new";
        StreamWriter file = new StreamWriter(filePath);
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
