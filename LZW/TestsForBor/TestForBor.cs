namespace TestsForBor;

using Bor;
public class TestsForBor
{
    Bor bor;
    [SetUp]
    public void Setup()
    {
        bor = new Bor();
    }

    [TestCaseSource(nameof(BorForTests))]
    public void TheAddedElementShouldbeFoundInBor(Bor bor)
    {
        Setup();
        bor.Add(bor, "end".ToCharArray(), 0, 2);
        Assert.True(bor.Contains("end".ToCharArray(), 0, 2), "Problems with the addition test!\n");
    }

    [TestCaseSource(nameof(BorForTests))]
    public void WhenAddingTwoLinesToBorTheBorShouldIncludeTwoLines(Bor bor)
    {
        Setup();
        bor.Add(bor, "endProgram".ToCharArray(), 0, 9);
        bor.Add(bor, "endFunction".ToCharArray(), 0, 10);
        Assert.True(bor.HowManyStringsInBor() == 2, "Problems with the prefix test!");
    }

    [TestCaseSource(nameof(BorForTests))]
    public void WhenAddingTheSymbolFlowInTheBorShouldBeCorrect(Bor bor)
    {
        Setup();
        bor.Add(bor, "a".ToCharArray(), 0, 0);
        bor.Add(bor, "b".ToCharArray(), 0, 0);
        Assert.True(bor.ReturnFlowByCharArray("b".ToCharArray(), 0, 0) == 1, "Problems with the prefix test!");
    }

    [TestCaseSource(nameof(BorForTests))]
    public void TheInitializedBorShouldBeEmpty(Bor bor)
    {
        Setup();
        Assert.False(bor.Contains("end".ToCharArray(), 0, 2), "Problems with the prefix test!");
    }

    [TestCaseSource(nameof(BorForTests))]
    public void ThereShouldBeNoThreadsInTheCreatedBor(Bor bor)
    {
        Setup();
        Assert.True(bor.ReturnFlowByCharArray("".ToCharArray(), 0, 0) == -1, "Problems with the prefix test!");
    }

    private static IEnumerable<TestCaseData> BorForTests
    => new TestCaseData[]
    {
        new TestCaseData(new Bor()),
    };
}