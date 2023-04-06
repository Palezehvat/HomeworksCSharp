namespace TestsFotGraph;

using NuGet.Frameworks;
using Routers;
public class Tests
{
    ListVertexes list;

    [SetUp]
    public void Setup()
    {
        list = new ListVertexes();
    }

    [TestCaseSource(nameof(ListForTest))]
    public void InStartNumberSetForAllVertexesShouldZero(ListVertexes list)
    {
        Graph graph = new Graph();
        graph.AddVertexes(3);
        graph.AddArcs(1, 2, 30);
        graph.AddArcs(1, 3, 20);
        graph.AddArcs(2, 3, 10);
        var anotherList = graph.ReturnListVertexes();
        for (int i = 1; i <= 3; ++i)
        {
            if (anotherList.FromWichSet(i) != 0)
            {
                Assert.Fail();
            }
        }
        Assert.True(true);
    }

    [TestCaseSource(nameof(ListForTest))]
    public void EmptyListVertexesShouldReturnMinusOneAfterTryToReturnNumberSetIfVertexNotInList(ListVertexes list)
    {
        Assert.True(list.FromWichSet(1) == -1);
    }

    [TestCaseSource(nameof(ListForTest))]
    public void ListVertexesShouldChangedNumberSet(ListVertexes list)
    {
        Graph graph = new Graph();
        graph.AddVertexes(3);
        graph.AddArcs(1, 2, 3);
        var anotherList = graph.ReturnListVertexes();
        anotherList.ChangeOneVertexSet(2, 10);
        Assert.True(anotherList.FromWichSet(2) == 10);
    }

    [TestCaseSource(nameof(ListForTest))]
    public void ListVertexesShouldChangedAllVertexesNumberSet(ListVertexes list)
    {
        Graph graph = new Graph();
        graph.AddVertexes(3);
        graph.AddArcs(1, 2, 3);
        var anotherList = graph.ReturnListVertexes();
        anotherList.ChangeNumbersSet(10, 0);
        for (int i = 1; i <= 3; ++i)
        {
            if (anotherList.FromWichSet(i) != 10)
            {
                Assert.Fail();
            }
        }
        Assert.True(true);
    }

    [Test]
    public void ListArcsShouldThrowExceptionIfTryToSortNullPointer()
    {
        var list = new ListArcs();
        Assert.Throws<NullPointerException>(() => list.SortListArcs(true));
    }

    private static IEnumerable<TestCaseData> ListForTest
    => new TestCaseData[]
    {
        new TestCaseData(new ListVertexes()),
    };
}