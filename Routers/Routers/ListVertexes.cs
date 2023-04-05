namespace Routers;

/// summary
/// Container for storing vertices and their belonging to a certain set
/// </summary>
public class ListVertexes
{
    private ListElement? Head;
    private ListElement? Tail;

    /// <summary>
    ///  Change number set for all vertexes with previous number set in list
    /// </summary>
    /// <param name="numberNewSet">Number new set for change</param>
    /// <param name="numberPreviousSet">Number previous set</param>
    public void ChangeNumbersSet(int numberNewSet, int numberPreviousSet)
    {
        var walker = Head;
        while(walker != null)
        {
            if (walker.InWichSet == numberPreviousSet)
            {
                walker.InWichSet = numberNewSet;
            }
            walker = walker.Next;
        }
    }

    /// <summary>
    /// Change number set for one vertex
    /// </summary>
    /// <param name="vertex">The vertex where we change the set value</param>
    /// <param name="numberSet">New set number</param>
    public void ChangeOneVertexSet(int vertex, int numberSet)
    {
        var walker = Head;
        while(walker != null)
        {
            if (walker.Vertex == vertex)
            {
                walker.InWichSet = numberSet;
            }
            walker = walker.Next;
        }
    }

    /// <summary>
    /// Returns the set number for a specific vertex
    /// </summary>
    /// <param name="vertex">The vertex for which we return</param>
    /// <returns>Return -1 if not found and the set number if found</returns>
    public int FromWichSet(int vertex)
    {
        var walker = Head;
        while(walker != null)
        {
            if (walker.Vertex == vertex)
            {
                return walker.InWichSet;
            }
            walker = walker.Next;
        }
        return -1;
    }

    /// <summary>
    /// Adds a new item to the list
    /// </summary>
    /// <param name="vertex">The number of the vertex to be added to the list</param>
    virtual public void AddElement(int vertex)
    {
        if (Head == null)
        {
            var item = new ListElement(vertex);
            Head = item;
            Tail = item;
        }
        else
        {
            var item = new ListElement(vertex);
            Tail.Next = item;
            Tail = item;
        }
    }

    private class ListElement
    {
        public ListElement(int vertex)
        {
            Vertex = vertex;
        }

        public int Vertex { get; set; }
        public ListElement? Next { get; set; }

        public int InWichSet { get; set; } 
    }
}
