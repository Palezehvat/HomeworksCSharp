namespace RoutersByGraph;

/// <summary>
/// A container consisting of two lists List Arcs, ListVertexes and its own size
/// </summary>
public class Graph
{
    /// <summary>
    /// Returns graph size
    /// </summary>
    /// <returns>Graph size</returns>
    /// <exception cref="NullPointerException">If the graph is empty throw exception</exception>
    public int Size() => sizeGraph;

    /// <summary>
    /// It is used as a wrapper for writing a graph to a file
    /// </summary>
    /// <param name="filePath">Location of the original file</param>
    /// <exception cref="NullPointerException">An empty or unfilled graph throws an exception</exception>
    public void WriteToFile(string filePath, string fileAfter)
    {
        if (edges == null || vertexes == null)
        {
            throw new NullGraphOrGraphComponentsException();
        }
        edges.WirteToFile(filePath);
    }

    /// <summary>
    /// Function to return ListArcs
    /// </summary>
    /// <returns>ListArcs in Graph</returns>
    public ListEdges? ReturnListArcs() => edges;


    /// <summary>
    /// Function to return ListVertexes
    /// </summary>
    /// <returns>ListVertexes</returns>
    public ListVertexes? ReturnListVertexes() => vertexes;

    /// <summary>
    /// Checks that the graph and its components are filled
    /// </summary>
    /// <returns>Returns true if the graph or its components are filled otherwise false</returns>
    public bool IsEmpty()
    {
        return edges == null || vertexes == null;
    }

    /// <summary>
    /// Adds paths from one vertex to another to the graph
    /// </summary>
    /// <param name="fromVertex">The vertex from which the path exits</param>
    /// <param name="toVertex">The vertex that the path is included in</param>
    /// <param name="sizeWay">Path Size</param>
    public void AddArcs(int fromVertex, int toVertex, int sizeWay)
    {

        if (edges == null)
        {
            edges = new ListEdges();
        }
        edges.AddElement(fromVertex, toVertex, sizeWay);
    }

    /// <summary>
    /// Initializes the list of vertices in the graph
    /// </summary>
    /// <param name="size">The size of the future graph</param>
    public void AddVertexes(int size)
    {
        if (vertexes == null)
        {
            vertexes = new ListVertexes();
        }
        sizeGraph = size;
        for(int i = 1; i <= sizeGraph; i++) 
        { 
            vertexes.AddElement(i);
        }
    }

    /// <summary>
    /// The shell of the Kraskala algorithm
    /// </summary>
    /// <param name="graph">Accepts the graph for which the algorithm will be applied</param>
    /// <returns>Returns true if the graph is connected and false otherwise</returns>
    /// <exception cref="NullPointerException">Throws an exception if the graph or its components are empty</exception>
    public bool KraskalAlgorithm(Graph graph)
    {
        if (edges == null)
        {
            throw new NullGraphOrGraphComponentsException();
        }
        return edges.KraskalAlgorithm(graph);
    }

    private ListVertexes? vertexes { get; set; }

    private ListEdges? edges { get; set; }

    private int sizeGraph { get; set; }

}