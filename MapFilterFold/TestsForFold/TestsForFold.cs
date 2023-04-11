namespace TestsForMap;

using MapFilterFold;

public class Tests
{
    FoldList fold;

    [SetUp]
    public void Setup()
    {
        fold = new FoldList();
    }

    [TestCaseSource(nameof(FoldForTest))]
    public void TheListOnIntShouldWorkCorrectly(FoldList fold)
    {
        List<int> list = new List<int> { 1, 2, 3 };
        var returnedNumber = fold.Fold(list, 1,(acc, elem) => acc * elem);
        Assert.IsTrue(returnedNumber == 6);
    }

    [TestCaseSource(nameof(FoldForTest))]
    public void TheListOnCharShouldWorkCorrectly(FoldList fold)
    {
        List<char> list = new List<char> { '1', '2', '3' };
        var returnedNumber = fold.Fold(list, '1', (acc, elem) => (char)(acc * elem));
        Assert.IsTrue(returnedNumber == '氶');
    }

    [TestCaseSource(nameof(FoldForTest))]
    public void AnEmptyListShouldFinishTheJobCorrectly(FoldList fold)
    {
        List<int> list = null;
        var returnedNumber = fold.Fold(list, 1, (acc, elem) => acc * elem);
        Assert.IsTrue(list == null);
    }

    private static IEnumerable<TestCaseData> FoldForTest
    => new TestCaseData[]
    {
        new TestCaseData(new FoldList())
    };
}