namespace TestsForRouters;

using Routers;

public class Tests
{
    Routers routers;

    [SetUp]
    public void Setup()
    {
        routers = new Routers();
    }

    [TestCaseSource(nameof(RoutersForTest))]
    public void RoutersShouldThrowAnExceptionIfThereIsAnInvalidCharacterInTheFile(Routers routers)
    {
        Assert.Throws<InvalidFileException>(() => routers.WorkWithFile(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithInvalidSymbol.txt")));
    }

    [TestCaseSource(nameof(RoutersForTest))]
    public void RoutersShouldThrowAnExceptionIfFirstSymbolIsNotNumberInTheFile(Routers routers)
    {
        Assert.Throws<InvalidFileException>(() => routers.WorkWithFile(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithInvalidFirstSymbol.txt")));
    }

    [TestCaseSource(nameof(RoutersForTest))]
    public void RoutersShouldThrowAnExceptionIfSpaceNotAferNotMainVertexInTheFile(Routers routers)
    {
        Assert.Throws<InvalidFileException>(() => routers.WorkWithFile(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithoutSpaceAfterVertexNotMain.txt")));
    }

    [TestCaseSource(nameof(RoutersForTest))]
    public void RoutersShouldThrowAnExceptionIfColonAbsentAfterMainVertexInTheFile(Routers routers)
    {
        Assert.Throws<InvalidFileException>(() => routers.WorkWithFile(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithoutColonAfterMainVertex.txt")));
    }

    [TestCaseSource(nameof(RoutersForTest))]
    public void RoutersShouldThrowAnExceptionIfSpaceAbsentAfterColonInTheFile(Routers routers)
    {
        Assert.Throws<InvalidFileException>(() => routers.WorkWithFile(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithoutSpaceAfterColon.txt")));
    }

    [TestCaseSource(nameof(RoutersForTest))]
    public void RoutersShouldThrowAnExceptionIfOpeningParenthesisAbsentAfterVertexWithSpaceInTheFile(Routers routers)
    {
        Assert.Throws<InvalidFileException>(() => routers.WorkWithFile(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithoutOpeningParenthesis.txt")));
    }

    [TestCaseSource(nameof(RoutersForTest))]
    public void RoutersShouldThrowAnExceptionIfNotNumberAfteropeningParenthesisInTheFile(Routers routers)
    {
        Assert.Throws<InvalidFileException>(() => routers.WorkWithFile(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithoutNumberAfterOpeningParenthesis.txt")));
    }

    [TestCaseSource(nameof(RoutersForTest))]
    public void RoutersShouldThrowAnExceptionIfCloseingParenthesisAbsentInTheFile(Routers routers)
    {
        Assert.Throws<InvalidFileException>(() => routers.WorkWithFile(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithoutCloseingParenthesis.txt")));
    }

    [TestCaseSource(nameof(RoutersForTest))]
    public void RoutersShouldThrowAnExceptionIfAfterCloseingParenthesisCommaAbsentInTheFile(Routers routers)
    {
        Assert.Throws<InvalidFileException>(() => routers.WorkWithFile(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithoutCommaAfterCloseingParenthesis.txt")));
    }

    [TestCaseSource(nameof(RoutersForTest))]
    public void RoutersShouldWorkCorrectlyWithSimpleExpression(Routers routers)
    {
        bool isLink = routers.WorkWithFile(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithFirstCorrect.txt"));
        if (!isLink)
        {
            Assert.Fail();
        }
        string afterAlgorithm = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithFirstCorrect.txt.new"));
        string correctResult = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithFirstCorrect.txt"));
        Assert.True(afterAlgorithm == correctResult);
    }

    [TestCaseSource(nameof(RoutersForTest))]
    public void RoutersShouldWorkCorrectlyWithSimpleExpressionWhereGraphNotLinked(Routers routers)
    {
        Assert.False(routers.WorkWithFile(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithNotLinkedGraph.txt")));
    }

    [TestCaseSource(nameof(RoutersForTest))]
    public void RoutersShouldWorkCorrectlyWithDificultExpressionWhereGraphNotLinked(Routers routers)
    {
        Assert.False(routers.WorkWithFile(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithNotLinkedGraphMoreDificult.txt")));
    }

    [TestCaseSource(nameof(RoutersForTest))]
    public void RoutersShouldWorkCorrectlyWithDificultExpression(Routers routers)
    {
        bool isLink = routers.WorkWithFile(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithMoreDifficultExpression.txt"));
        if (!isLink)
        {
            Assert.Fail();
        }
        string afterAlgorithm = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithMoreDifficultExpression.txt.new"));
        string correctResult = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForRouters", "fileWithCorrectExpressionForMoreDifficultExpression.txt"));
        Assert.True(afterAlgorithm == correctResult);
    }

    private static IEnumerable<TestCaseData> RoutersForTest
    => new TestCaseData[]
    {
        new TestCaseData(new Routers()),
    };
}