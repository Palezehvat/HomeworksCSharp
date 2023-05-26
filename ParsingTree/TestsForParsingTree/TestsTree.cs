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

    [Test]
    public void InTheUsualExampleTheTreeShouldCorrectlyCalculateTheValue()
    {
        tree.TreeExpression("+ 2 3");
        Assert.True(tree.Calcuate() == 5);
    }

    [Test]
    public void InTheNormalExampleTheTreeShouldCorrectlyCalculateTheValue()
    {
        tree.TreeExpression("(* (+ 2 3) (+ 5 7)");
        Assert.True(tree.Calcuate() == 60);
    }

    [Test]
    public void WhenAnEmptyStrinIsReceivedTheTreeShouldThrowAnException()
    {
        Assert.Throws<NullReferenceException>(() => tree.TreeExpression(""));
    }

    [Test]
    public void WhenReceivingAnIncorrectStringWithTheAbsenceOfASignTheTreeShouldThrowAnException()
    {
        Assert.Throws<InvalidExpressionException>(() => tree.TreeExpression(" 1 2"));
    }

    [Test]
    public void WhenReceivingAnIncorrectMoreDifficultStringWithTheAbsenceOfASignTheTreeShouldThrowAnException()
    {
        Assert.Throws<InvalidExpressionException>(() => tree.TreeExpression("(* (4 5) 2)"));
    }

    [Test]
    public void WhenReceivingAnIncorrectStringWithTheTheAbsenceOfANumberTheTreeShouldThrowAnException()
    {
        Assert.Throws<InvalidExpressionException>(() => tree.TreeExpression("+ 2"));
    }

    [Test]
    public void WhenReceivingAnIncorrectMoreDifficultStringWithTheTheAbsenceOfANumberTheTreeShouldThrowAnException()
    {
        Assert.Throws<InvalidExpressionException>(() => tree.TreeExpression("(* (+ 2 3) )"));
    }

    [Test]
    public void WhenReceivingAnDifficultStringWithInvalidCharactersTheTreeShouldThrowAnException()
    {
        Assert.Throws<InvalidExpressionException>(() => tree.TreeExpression("(* (+ 2 3) p 2)"));
    }

    [Test]
    public void WhenTryingToDivideByZeroTheTreeShouldThrowAnException()
    {
        tree.TreeExpression("/ 2 0");
        Assert.Throws<ArgumentException>(() => tree.Calcuate());
    }

    [Test]
    public void TheTreeShouldWorkCorrectlyWithNegativeNumbers()
    {
        tree.TreeExpression("+ 2 -3");
        Assert.True(tree.Calcuate() == -1);
    }
}