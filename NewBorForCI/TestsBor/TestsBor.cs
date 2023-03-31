namespace Bor;

public class Tests
{
    [SetUp]
    public void Setup()
    {

    }

    Bor bor;

    [Test]
    public void TestInitialize()
    {
        bor = new Bor();
    }

    [Test]
    public void TheAddedElementShouldbeFoundInBor()
    {
        TestInitialize();
        bor.Add("end");
        Assert.That(bor.Contains("end"), "Problems with the addition test!\n");
    }

    [Test]
    public void ByAddingAndRemovingAnElementItShouldNotStayInBor()
    {
        TestInitialize();
        bor.Add("end");
        bor.Remove("end");
        Assert.That(!bor.Contains("end"), "Problems with the deletion test!\n");
    }

    [Test]
    public void AddTwoStringsWithTheSamePrefixBorTheNumberOfStringsWithThisPrefixIsTwo()
    {
        TestInitialize();
        bor.Add("endProgram");
        bor.Add("endFunction");
        Assert.That(bor.HowManyStartsWithPrefix("end") == 2, "Problems with the prefix test!");
    }

    [Test]
    public void WhenEnteringDifferentStringsTheNumberOfStringsWithTheSamePrefixNotZeroMustBeOne()
    {
        TestInitialize();
        bor.Add("aaaaa");
        bor.Add("bbbbb");
        bor.Add("ccccc");
        Assert.That(bor.HowManyStartsWithPrefix("a") == 1, "Problems with the prefix test!");
    }

    [Test]
    public void TheAnswerToANoExistentPrefixInTheBorShouldBeOne()
    {
        TestInitialize();
        bor.Add("adadasd");
        Assert.That(bor.HowManyStartsWithPrefix("dsdsd") == 0, "Problems with non-existent prefix!");
    }

    [Test]
    public void AnEmptyPrefixShouldGiveOuAllTheLinesInTheBor()
    {
        TestInitialize();
        bor.Add("adads");
        bor.Add("ddsds");
        bor.Add("End");
        Assert.That(bor.HowManyStartsWithPrefix("") == 3, "Problems with null prefix!");
    }

    [Test]
    public void EmptyBorShouldGiveZeroStringsInBor()
    {
        TestInitialize();
        Assert.That(bor.HowManyStartsWithPrefix("") == 0, "Problems with null bor");
    }
}