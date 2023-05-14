namespace Trie;

public class Tests
{
    private Trie? trie;

    [Test]
    public void TestInitialize()
    {
        trie = new();
        if (trie == null)
        {
            throw new NullReferenceException();
        }
    }

    [Test]
    public void TheAddedElementShouldbeFoundInBor()
    {
        TestInitialize();
        trie.Add("end");
        Assert.True(trie.Contains("end"));
    }

    [Test]
    public void ByAddingAndRemovingAnElementItShouldNotStayInBor()
    {
        TestInitialize();
        trie.Add("end");
        trie.Remove("end");
        Assert.False(trie.Contains("end"));
    }

    [Test]
    public void AddTwoStringsWithTheSamePrefixBorTheNumberOfStringsWithThisPrefixIsTwo()
    {
        TestInitialize();
        trie.Add("endProgram");
        trie.Add("endFunction");
        Assert.That(trie.HowManyStartsWithPrefix("end") == 2);
    }

    [Test]
    public void WhenEnteringDifferentStringsTheNumberOfStringsWithTheSamePrefixNotZeroMustBeOne()
    {
        TestInitialize();
        trie.Add("aaaaa");
        trie.Add("bbbbb");
        trie.Add("ccccc");
        Assert.That(trie.HowManyStartsWithPrefix("a") == 1);
    }

    [Test]
    public void TheAnswerToANoExistentPrefixInTheBorShouldBeOne()
    {
        TestInitialize();
        trie.Add("adadasd");
        Assert.That(trie.HowManyStartsWithPrefix("dsdsd") == 0);
    }

    [Test]
    public void AnEmptyPrefixShouldGiveOuAllTheLinesInTheBor()
    {
        TestInitialize();
        trie.Add("adads");
        trie.Add("ddsds");
        trie.Add("End");
        Assert.That(trie.HowManyStartsWithPrefix("") == 3);
    }

    [Test]
    public void EmptyBorShouldGiveZeroStringsInBor()
    {
        TestInitialize();
        Assert.That(trie.HowManyStartsWithPrefix("") == 0);
    }
}