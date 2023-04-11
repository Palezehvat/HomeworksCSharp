namespace TestsForMap;

using MapFilterFold;

public class Tests
{
    MapList map;

    [SetUp]
    public void Setup()
    {
        map = new MapList();
    }

    [TestCaseSource(nameof(MapForTest))]
    public void TheListOnIntShouldWorkCorrectly(MapList map)
    {
        List<int> list = new List<int> { 1, 2, 3 };
        List<int> listCheck = new List<int> { 2, 4, 6 };
        list = map.Map(list, x => x * 2);
        Assert.IsTrue(list.SequenceEqual(listCheck));
    }

    [TestCaseSource(nameof(MapForTest))]
    public void TheListOnCharShouldWorkCorrectly(MapList map)
    {
        List<char> list = new List<char> { '1', '2', '3' };
        List<char> listCheck = new List<char> { 'b', 'd', 'f' };
        list = map.Map(list, x => (char)(x * 2));
        Assert.IsTrue(list.SequenceEqual(listCheck));
    }

    [TestCaseSource(nameof(MapForTest))]
    public void AnEmptyListShouldFinishTheJobCorrectly(MapList map)
    {
        List<int> list = null;
        list = map.Map(list, x => (x * 2));
        Assert.IsTrue(list == null);
    }

    private static IEnumerable<TestCaseData> MapForTest
    => new TestCaseData[]
    {
        new TestCaseData(new MapList())
    };
}