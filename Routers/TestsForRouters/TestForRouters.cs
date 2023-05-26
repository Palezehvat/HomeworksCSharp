namespace TestsForRouters;

using RoutersByGraph;

public class Tests
{
    private Routers routers;

    [SetUp]
    public void Setup()
    {
        routers = new Routers();
    }

    [Test]
    public void RoutersShouldThrowAnExceptionIfThereIsAnInvalidCharacterInTheFile()
    {
        Assert.Throws<InvalidFileException>(() => routers.WorkWithFile(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithInvalidSymbol.txt"), "dsd"));
    }

    [Test]
    public void RoutersShouldThrowAnExceptionIfFirstSymbolIsNotNumberInTheFile()
    {
        Assert.Throws<InvalidFileException>(() => routers.WorkWithFile(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithInvalidFirstSymbol.txt"), "sdsd"));
    }

    [Test]
    public void RoutersShouldThrowAnExceptionIfSpaceNotAferNotMainVertexInTheFile()
    {
        Assert.Throws<InvalidFileException>(() => routers.WorkWithFile(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithoutSpaceAfterVertexNotMain.txt"), "dsds"));
    }

    [Test]
    public void RoutersShouldThrowAnExceptionIfColonAbsentAfterMainVertexInTheFile()
    {
        Assert.Throws<InvalidFileException>(() => routers.WorkWithFile(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithoutColonAfterMainVertex.txt"), "sddsd"));
    }

    [Test]
    public void RoutersShouldThrowAnExceptionIfSpaceAbsentAfterColonInTheFile()
    {
        Assert.Throws<InvalidFileException>(() => routers.WorkWithFile(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithoutSpaceAfterColon.txt"), "sdsd"));
    }

    [Test]
    public void RoutersShouldThrowAnExceptionIfOpeningParenthesisAbsentAfterVertexWithSpaceInTheFile()
    {
        Assert.Throws<InvalidFileException>(() => routers.WorkWithFile(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithoutOpeningParenthesis.txt"), "sdsd"));
    }

    [Test]
    public void RoutersShouldThrowAnExceptionIfNotNumberAfteropeningParenthesisInTheFile()
    {
        Assert.Throws<InvalidFileException>(() => routers.WorkWithFile(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithoutNumberAfterOpeningParenthesis.txt"), "sdsd"));
    }

    [Test]
    public void RoutersShouldThrowAnExceptionIfCloseingParenthesisAbsentInTheFile()
    {
        Assert.Throws<InvalidFileException>(() => routers.WorkWithFile(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithoutCloseingParenthesis.txt"), "sdsd"));
    }

    [Test]
    public void RoutersShouldThrowAnExceptionIfAfterCloseingParenthesisCommaAbsentInTheFile()
    {
        Assert.Throws<InvalidFileException>(() => routers.WorkWithFile(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithoutCommaAfterCloseingParenthesis.txt"), "sdsd"));
    }

    [Test]
    public void RoutersShouldWorkCorrectlyWithSimpleExpression()
    {
        bool isLink = routers.WorkWithFile(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithFirstCorrect.txt"), Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithFirstCorrect.txt.new"));
        if (!isLink)
        {
            Assert.Fail();
        }
        string afterAlgorithm = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithFirstCorrect.txt.new"));
        string correctResult = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithFirstCorrect.txt"));
        Assert.That(correctResult, Is.EqualTo(afterAlgorithm));
    }

    [Test]
    public void RoutersShouldWorkCorrectlyWithSimpleExpressionWhereGraphNotLinked()
    {
        Assert.False(routers.WorkWithFile(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithNotLinkedGraph.txt"), Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithNotLinkedGraph.txt.new")));
    }

    [Test]
    public void RoutersShouldWorkCorrectlyWithDificultExpressionWhereGraphNotLinked()
    {
        Assert.False(routers.WorkWithFile(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithNotLinkedGraphMoreDificult.txt"), "sdsd"));
    }

    [Test]
    public void RoutersShouldWorkCorrectlyWithDificultExpression()
    {
        bool isLink = routers.WorkWithFile(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithMoreDifficultExpression.txt"), Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithMoreDifficultExpression.txt.new"));
        if (!isLink)
        {
            Assert.Fail();
        }
        string afterAlgorithm = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithMoreDifficultExpression.txt.new"));
        string correctResult = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithCorrectExpressionForMoreDifficultExpression.txt"));
        Assert.That(correctResult, Is.EqualTo(afterAlgorithm));
    }
}