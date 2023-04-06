namespace TestsFotGraph;

using Routers;
public class Tests
{
    Graph graph;

    [SetUp]
    public void Setup()
    {
        graph = new Graph();
    }

    [TestCaseSource(nameof(GraphForTest))]
    public void EmptyGraphShouldBeEmpty(Graph graph)
    {
        Assert.True(graph.IsEmpty());
    }

    [TestCaseSource(nameof(GraphForTest))]
    public void GraphShouldThrowExceptionAfterTryGetSizeWhileItEmpty(Graph graph)
    {
        Assert.Throws<NullPointerException>(() => graph.ReturnSizeGraph());
    }

    [TestCaseSource(nameof(GraphForTest))]
    public void GraphShouldReturnNullAfterTryGetListArcsWhileItEmpty(Graph graph)
    {
        Assert.True(graph.ReturnListArcs() == null);
    }

    [TestCaseSource(nameof(GraphForTest))]
    public void GraphShouldReturnNullAfterTryGetListVertexesWhileItEmpty(Graph graph)
    {
        Assert.True(graph.ReturnListVertexes() == null);
    }

    [TestCaseSource(nameof(GraphForTest))]
    public void GraphShouldThrowExceptionAfterTryWriteInFileWhileGraphEmpty(Graph graph)
    {
        Assert.Throws<NullPointerException>(() => graph.WriteToFile("v_v"));
    }

    [TestCaseSource(nameof(GraphForTest))]
    public void GraphShouldThrowExceptionAfterTryUseKraskalAlgorithmWhileGraphEmpty(Graph graph)
    {
        Assert.Throws<NullPointerException>(() => graph.KraskalAlgorithm(graph));
    }

    [TestCaseSource(nameof(GraphForTest))]
    public void GraphShouldReturnCorrectSizeAfterAddding(Graph graph)
    {
        graph.AddArcs(1, 2, 3);
        graph.AddVertexes(2);
        Assert.True(graph.ReturnSizeGraph() == 2);
    }

    [TestCaseSource(nameof(GraphForTest))]
    public void GraphShouldBeNotEmptyAfterAddding(Graph graph)
    {
        graph.AddArcs(1, 2, 3);
        graph.AddVertexes(2);
        Assert.False(graph.IsEmpty());
    }

    private static IEnumerable<TestCaseData> GraphForTest
    => new TestCaseData[]
    {
        new TestCaseData(new Graph()),
    };
}