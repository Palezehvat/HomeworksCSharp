namespace TestsParsingTree;

using ParsingTree;

public class Tests
{
    Tree tree;
    [SetUp]
    public void Setup()
    {
        tree = new Tree();
    }

    [TestCaseSource(nameof(TreeForTest))]
    public void InTheUsualExampleTheTreeShouldCorrectlyCalculateTheValue(Tree tree)
    {
        tree.TreeExpression("+ 2 3");
        Assert.True(tree.Calcuate() == 5);
    }

    [TestCaseSource(nameof(TreeForTest))]
    public void InTheNormalExampleTheTreeShouldCorrectlyCalculateTheValue(Tree tree)
    {
        tree.TreeExpression("(* (+ 2 3) 2)");
        Assert.True(tree.Calcuate() == 10);
    }

    [TestCaseSource(nameof(TreeForTest))]
    public void WhenAnEmptyStrinIsReceivedTheTreeShouldThrowAnException(Tree tree)
    {
        Assert.Throws<NullReferenceException>(() => tree.TreeExpression(""));
    }

    [TestCaseSource(nameof(TreeForTest))]
    public void WhenReceivingAnIncorrectStringWithTheAbsenceOfASignTheTreeShouldThrowAnException(Tree tree)
    {
        Assert.Throws<InvalidExpressionException>(() => tree.TreeExpression(" 1 2"));
    }

    [TestCaseSource(nameof(TreeForTest))]
    public void WhenReceivingAnIncorrectMoreDifficultStringWithTheAbsenceOfASignTheTreeShouldThrowAnException(Tree tree)
    {
        Assert.Throws<InvalidExpressionException>(() => tree.TreeExpression("(* (4 5) 2)"));
    }

    [TestCaseSource(nameof(TreeForTest))]
    public void WhenReceivingAnIncorrectStringWithTheTheAbsenceOfANumberTheTreeShouldThrowAnException(Tree tree)
    {
        Assert.Throws<InvalidExpressionException>(() => tree.TreeExpression("+ 2"));
    }

    [TestCaseSource(nameof(TreeForTest))]
    public void WhenReceivingAnIncorrectMoreDifficultStringWithTheTheAbsenceOfANumberTheTreeShouldThrowAnException(Tree tree)
    {
        Assert.Throws<InvalidExpressionException>(() => tree.TreeExpression("(* (+ 2 3) )"));
    }

    [TestCaseSource(nameof(TreeForTest))]
    public void WhenReceivingAnDifficultStringWithInvalidCharactersTheTreeShouldThrowAnException(Tree tree)
    {
        Assert.Throws<InvalidExpressionException>(() => tree.TreeExpression("(* (+ 2 3) p 2)"));
    }

    [TestCaseSource(nameof(TreeForTest))]
    public void WhenTryingToDivideByZeroTheTreeShouldThrowAnException(Tree tree)
    {
        tree.TreeExpression("/ 2 0");
        Assert.Throws<ArgumentException>(() => tree.Calcuate());
    }

    [TestCaseSource(nameof(TreeForTest))]
    public void TheTreeShouldWorkCorrectlyWithNegativeNumbers(Tree tree)
    {
        tree.TreeExpression("+ 2 -3");
        Assert.True(tree.Calcuate() == -1);
    }

    private static IEnumerable<TestCaseData> TreeForTest
    => new TestCaseData[]
    {
        new TestCaseData(new Tree()),
    };
}