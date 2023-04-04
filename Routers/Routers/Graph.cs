using System.Reflection.Metadata;

namespace Routers;

public class Graph
{
    public void AddVertexWithBandwidthSize(int fromVertex, int toVertex, int bandwidthSize)
    {
        if (GraphByList == null || GraphByList.ListWithVertexes == null)
        {
            throw new NullPointerException();
        }
        GraphByList.ListWithVertexes.AddAnArr(GraphByList.ListWithVertexes, fromVertex, toVertex, bandwidthSize);
    }

    public void AddStandartListWithAllVertexes(int sizeVertexes)
    {
        GraphByList = new ListVertexes();
        GraphByList.SizeGraph = sizeVertexes;
        GraphByList.ListWithVertexes = new List();
        for(int i = 1; i < sizeVertexes + 1; i++)
        {
            GraphByList.ListWithVertexes.AddElement(GraphByList.ListWithVertexes, i, 0);
        }
    }

    public void PrintGraph()
    {
        if (GraphByList == null || GraphByList.ListWithVertexes == null)
        {
            throw new NullPointerException();
        }
        GraphByList.ListWithVertexes.PrintGraphByList(GraphByList.ListWithVertexes);
    }

    public void SortListsInGraph()
    {
        ;
    }

    public void FloydsAlgorithm()
    {
        SortListsInGraph();
        if (GraphByList == null || GraphByList.ListWithVertexes == null)
        {
            throw new NullPointerException();
        }
        int sizeGraph = GraphByList.SizeGraph;
        for(int k = 0; k < sizeGraph; ++k)
        {
            for(int i = 0; i < sizeGraph; ++i)
            {
                for(int j = 0; j < sizeGraph; ++j)
                {

                }
            }
        }
    }

    private class ListVertexes
    {
        public List? ListWithVertexes { get; set; }
        public int SizeGraph { get; set; }
    }
    private ListVertexes? GraphByList { get; set; }
}
