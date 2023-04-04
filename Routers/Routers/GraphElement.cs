namespace Routers;

public class GraphElement
{
    public GraphElement(int vertex, int bandwidthSize)
    {
        Vertex = vertex;
        BandwidthSize = bandwidthSize;
    }

    public int Vertex { get; set; }
    public List Vertexes { get; set; }
    public int BandwidthSize { get; set; }
}