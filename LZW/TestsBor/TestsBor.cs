namespace TestsForBor;

using Bor;

public class TestsForBor
{
    private Bor bor;

    [SetUp]
    public void Setup()
    {
        bor = new Bor();
    }

    [Test]
    public void TheAddedElementShouldbeFoundInBor()
    {
        Setup();
        bor.Add("end".ToCharArray(), 0, 2);
        Assert.True(bor.Contains("end".ToCharArray(), 0, 2), "Problems with the addition test!\n");
    }

    [Test]
    public void WhenAddingTwoLinesToBorTheBorShouldIncludeTwoLines()
    {
        Setup();
        bor.Add("endProgram".ToCharArray(), 0, 9);
        bor.Add("endFunction".ToCharArray(), 0, 10);
        Assert.True(bor.HowManyStringsInBor() == 2, "Problems with the prefix test!");
    }

    [Test]
    public void WhenAddingTheSymbolFlowInTheBorShouldBeCorrect()
    {
        Setup();
        bor.Add("a".ToCharArray(), 0, 0);
        bor.Add("b".ToCharArray(), 0, 0);
        Assert.True(bor.ReturnSymbolCodeByCharArray("b".ToCharArray(), 0, 0) == 1, "Problems with the prefix test!");
    }

    [Test]
    public void TheInitializedBorShouldBeEmpty()
    {
        Setup();
        Assert.False(bor.Contains("end".ToCharArray(), 0, 2), "Problems with the prefix test!");
    }

    [Test]
    public void ThereShouldBeNoThreadsInTheCreatedBor()
    {
        Assert.True(bor.ReturnSymbolCodeByCharArray("".ToCharArray(), 0, 0) == -1, "Problems with the prefix test!");
    }

}