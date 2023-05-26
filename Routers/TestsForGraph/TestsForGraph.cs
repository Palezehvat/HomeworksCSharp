namespace TestsFotGraph;

using RoutersByGraph;

public class Tests
{
    private Graph graph;

    [SetUp]
    public void Setup()
    {
        graph = new Graph();
    }

    [Test]
    public void EmptyGraphShouldBeEmpty()
    {
        Assert.True(graph.IsEmpty());
    }

    [Test]
    public void GraphShouldThrowExceptionAfterTryGetSizeWhileItEmpty()
    {
        Assert.That(graph.Size(), Is.EqualTo(0));
    }

    [Test]
    public void GraphShouldReturnNullAfterTryGetListArcsWhileItEmpty()
    {
        Assert.That(graph.ReturnListArcs(), Is.EqualTo(null));
    }

    [Test]
    public void GraphShouldReturnNullAfterTryGetListVertexesWhileItEmpty()
    {
        Assert.That(graph.ReturnListVertexes(), Is.EqualTo(null));
    }

    [Test]
    public void GraphShouldThrowExceptionAfterTryWriteInFileWhileGraphEmpty()
    {
        Assert.Throws<NullGraphOrGraphComponentsException>(() => graph.WriteToFile("v_v", "^_^"));
    }

    [Test]
    public void GraphShouldThrowExceptionAfterTryUseKraskalAlgorithmWhileGraphEmpty()
    {
        Assert.Throws<NullGraphOrGraphComponentsException>(() => graph.KraskalAlgorithm(graph));
    }

    [Test]
    public void GraphShouldReturnCorrectSizeAfterAddding()
    {
        graph.AddArcs(1, 2, 3);
        graph.AddVertexes(2);
        Assert.That(2, Is.EqualTo(graph.Size()));
    }

    [Test]
    public void GraphShouldBeNotEmptyAfterAddding()
    {
        graph.AddArcs(1, 2, 3);
        graph.AddVertexes(2);
        Assert.False(graph.IsEmpty());
    }
}