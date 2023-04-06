namespace Routers;

/// <summary>
/// A container consisting of two lists List Arcs, ListVertexes and its own size
/// </summary>
public class Graph
{
    /// <summary>
    /// Returns graph size
    /// </summary>
    /// <returns>Graph size</returns>
    /// <exception cref="NullPointerException">If graph null throw exception</exception>
    public int ReturnSizeGraph()
    {
        if (IsEmpty())
        {
            throw new NullPointerException();
        }
        return GraphByList.sizeGraph;
    }

    /// <summary>
    /// It is used as a wrapper for writing a graph to a file
    /// </summary>
    /// <param name="filePath">Location of the original file</param>
    /// <exception cref="NullPointerException">An empty or unfilled graph throws an exception</exception>
    public void WriteToFile(string filePath, string fileAfter)
    {
        if (IsEmpty())
        {
            throw new NullPointerException();
        }
        GraphByList.Arcs.WirteToFile(filePath);
    }

    /// <summary>
    /// Function to return ListArcs
    /// </summary>
    /// <returns>ListArcs in Graph</returns>
    public ListArcs ReturnListArcs()
    {
        return !IsEmpty() ? GraphByList.Arcs : null;
    }

    /// <summary>
    /// Function to return ListVertexes
    /// </summary>
    /// <returns>ListVertexes</returns>
    public ListVertexes ReturnListVertexes()
    {
        return !IsEmpty() ? GraphByList.Vertexes : null;
    }

    /// <summary>
    /// Checks that the graph and its components are filled
    /// </summary>
    /// <returns>Returns true if the graph or its components are filled otherwise false</returns>
    public bool IsEmpty()
    {
        return GraphByList == null || GraphByList.Arcs == null || GraphByList.Vertexes == null;
    }

    /// <summary>
    /// Adds paths from one vertex to another to the graph
    /// </summary>
    /// <param name="fromVertex">The vertex from which the path exits</param>
    /// <param name="toVertex">The vertex that the path is included in</param>
    /// <param name="sizeWay">Path Size</param>
    public void AddArcs(int fromVertex, int toVertex, int sizeWay)
    {
        if(GraphByList == null)
        {
            GraphByList = new GraphElement();
        }
        if (GraphByList.Arcs == null)
        {
            GraphByList.Arcs = new ListArcs();
        }
        GraphByList.Arcs.AddElement(fromVertex, toVertex, sizeWay);
    }

    /// <summary>
    /// Initializes the list of vertices in the graph
    /// </summary>
    /// <param name="sizeGraph">The size of the future graph</param>
    public void AddVertexes(int sizeGraph)
    {
        if (GraphByList == null)
        {
            GraphByList = new GraphElement();
        }
        if (GraphByList.Vertexes == null)
        {
            GraphByList.Vertexes = new ListVertexes();
        }
        GraphByList.sizeGraph = sizeGraph;
        for(int i = 1; i <= sizeGraph; i++) 
        { 
            GraphByList.Vertexes.AddElement(i);
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
        if (graph.IsEmpty())
        {
            throw new NullPointerException();
        }
        return graph.GraphByList.Arcs.KraskalAlgorithm(graph);
    }

    private class GraphElement
    {
        public ListVertexes? Vertexes { get; set; }

        public ListArcs? Arcs { get; set; }

        public int sizeGraph { get; set; }
    }
    private GraphElement? GraphByList { get; set; }
}