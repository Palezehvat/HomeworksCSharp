namespace TestsFotGraph;

using RoutersByGraph;

public class Tests
{
    private ListVertexes list;

    [SetUp]
    public void Setup()
    {
        list = new ListVertexes();
    }

    [Test]
    public void InStartNumberSetForAllVertexesShouldZero()
    {
        Graph graph = new Graph();
        graph.AddVertexes(3);
        graph.AddArcs(1, 2, 30);
        graph.AddArcs(1, 3, 20);
        graph.AddArcs(2, 3, 10);
        var anotherList = graph.ReturnListVertexes();
        for (int i = 1; i <= 3; ++i)
        {
            Assert.AreEqual(anotherList.SearchForASuitableSet(i), 0);
        }
    }

    [Test]
    public void EmptyListVertexesShouldReturnMinusOneAfterTryToReturnNumberSetIfVertexNotInList()
    {
        Assert.True(list.SearchForASuitableSet(1) == -1);
    }

    [Test]
    public void ListVertexesShouldChangedNumberSet()
    {
        Graph graph = new Graph();
        graph.AddVertexes(3);
        graph.AddArcs(1, 2, 3);
        var anotherList = graph.ReturnListVertexes();
        anotherList.ChangeOneVertexSet(2, 10);
        Assert.AreEqual(anotherList.SearchForASuitableSet(2), 10);
    }

    [Test]
    public void ListVertexesShouldChangedAllVertexesNumberSet()
    {
        Graph graph = new Graph();
        graph.AddVertexes(3);
        graph.AddArcs(1, 2, 3);
        var anotherList = graph.ReturnListVertexes();
        anotherList.ChangeNumbersSet(10, 0);
        for (int i = 1; i <= 3; ++i)
        {
            Assert.AreEqual(anotherList.SearchForASuitableSet(i), 10);
        }
    }

    [Test]
    public void ListArcsShouldThrowExceptionIfTryToSortNullPointer()
    {
        var list = new ListEdges();
        Assert.Throws< NullGraphOrGraphComponentsException>(() => list.SortListArcs(true));
    }
}