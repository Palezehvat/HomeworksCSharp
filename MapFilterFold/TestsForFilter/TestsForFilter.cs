namespace TestsForMap;

using MapFilterFold;

public class Tests
{
    FilterList map;

    [SetUp]
    public void Setup()
    {
        map = new FilterList();
    }

    [TestCaseSource(nameof(FilterForTest))]
    public void TheListOnIntShouldWorkCorrectly(FilterList filter)
    {
        List<int> list = new List<int> { 1, 2, 3 };
        List<int> listCheck = new List<int> { 2 };
        list = filter.Filter(list, x => x % 2 == 0);
        Assert.IsTrue(list.SequenceEqual(listCheck));
    }

    [TestCaseSource(nameof(FilterForTest))]
    public void TheListOnCharShouldWorkCorrectly(FilterList filter)
    {
        List<char> list = new List<char> { '2', '3', '4' };
        List<char> listCheck = new List<char> { '2', '4' };
        list = filter.Filter(list, x => x % 2 == 0);
        Assert.IsTrue(list.SequenceEqual(listCheck));
    }

    [TestCaseSource(nameof(FilterForTest))]
    public void AnEmptyListShouldFinishTheJobCorrectly(FilterList filter)
    {
        List<int> list = null;
        list = filter.Filter(list, x => x % 2 == 0);
        Assert.IsTrue(list == null);
    }

    private static IEnumerable<TestCaseData> FilterForTest
    => new TestCaseData[]
    {
        new TestCaseData(new FilterList())
    };
}