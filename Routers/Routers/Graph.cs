using System.Reflection.Metadata;
using System.Linq;
using System.Collections.Generic;

namespace Routers;

public class Graph
{
    public void WriteToFile(string filePath)
    {
        if (IsEmpty())
        {
            throw new NullPointerException();
        }
        GraphByList.Arcs.WirteToFile(filePath);
    }

    public ListArcs ReturnListArcs()
    {
        return !IsEmpty() ? GraphByList.Arcs : null;
    }

    public ListVertexes ReturnListVertexes()
    {
        return !IsEmpty() ? GraphByList.Vertexes : null;
    }

    public bool IsEmpty()
    {
        return GraphByList == null || GraphByList.Arcs == null || GraphByList.Vertexes == null;
    }

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