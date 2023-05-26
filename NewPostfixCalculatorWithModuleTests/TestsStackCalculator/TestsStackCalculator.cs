namespace TestsForStackCalculator;

using StackCalculator;
using System.Numerics;

public class Tests
{
    private const double delta = 0.0000000000001;


    [TestCaseSource(nameof(Stacks))]
    public void TheCalculatorShouldWorkCorrectlyToReturnTheCorrectValueOnASimpleExample(Stack stack)
    {
        var (isCorrect, number) = PostfixCalculator.Calculate("1 2 +", stack);
        Assert.That(true, Is.EqualTo(isCorrect));
        Assert.That(3, Is.EqualTo(number));
    }

    [TestCaseSource(nameof(Stacks))]
    public void OnTheWrongLineTheCalculatorShouldReturnAnError(Stack stack)
    {
        var (isCorrect, _) = PostfixCalculator.Calculate("1 2", stack);
        Assert.IsFalse(isCorrect);
    }

    [TestCaseSource(nameof(Stacks))]
    public void TheFirstValueIsPositiveTheSecondIsNegativeTheCalculatorShouldReturnTheCorrectValue(Stack stack)
    {
        var (isCorrect, number) = PostfixCalculator.Calculate("1 -2 +", stack);
        Assert.That(isCorrect);
        Assert.That(-1, Is.EqualTo(number));
    }

    [TestCaseSource(nameof(Stacks))]
    public void TheSecondValueIsPositiveTheFirstIsNegativeTheCalculatorShouldReturnTheCorrectValue(Stack stack)
    {
        var (isCorrect, number) = PostfixCalculator.Calculate("-1 2 +", stack);
        Assert.That(isCorrect);
        Assert.That(1, Is.EqualTo(number));
    }

    [TestCaseSource(nameof(Stacks))]
    public void TheSecondValueIsNegativeTheFirstIsNegativeTheCalculatorShouldReturnTheCorrectValue(Stack stack)
    {
        var (isCorrect, number) = PostfixCalculator.Calculate("-1 -2 +", stack);
        Assert.That(isCorrect);
        Assert.That(-3, Is.EqualTo(number));
    }

    [TestCaseSource(nameof(Stacks))]
    public void TheStackCalculatorShouldWorkCorrectlyWithTheDifference(Stack stack)
    {
        var (isCorrect, number) = PostfixCalculator.Calculate("1 2 -", stack);
        Assert.That(isCorrect);
        Assert.That(-1, Is.EqualTo(number));
    }

    [TestCaseSource(nameof(Stacks))]
    public void TheStackCalculatorShouldWorkCorrectlyWithTheMultiplication(Stack stack)
    {
        var (isCorrect, number) = PostfixCalculator.Calculate("2 3 *", stack);
        Assert.That(isCorrect);
        Assert.That(6, Is.EqualTo(number));
    }

    [TestCaseSource(nameof(Stacks))]
    public void TheStackCalculatorShouldWorkCorrectlyWithTheDivision(Stack stack)
    {
        var (isCorrect, number) = PostfixCalculator.Calculate("5 2 /", stack);
        Assert.That(isCorrect);
        Assert.That(Math.Abs(number - 2.5) < delta);
    }

    [TestCaseSource(nameof(Stacks))]
    public void TheStackCalculatorShouldGiveAnErrorWhenReceivingAnemptyString(Stack stack)
    {
        var (isCorrect, _) = PostfixCalculator.Calculate("", stack);
        Assert.IsFalse(isCorrect);
    }

    [TestCaseSource(nameof(Stacks))]
    public void TheStack�alculatorShouldCorrectlyCalculateComplexExpressions(Stack stack)
    {
        var (isCorrect, number) = PostfixCalculator.Calculate("1 2 + 3 *", stack);
        Assert.That(isCorrect);
        Assert.That(9, Is.EqualTo(number));
    }

    [TestCaseSource(nameof(Stacks))]
    public void TheStackCalculatorShouldGiveAnErrorWhenReceivingAStringWithCharactersThatWereNotExpected(Stack stack)
    {
        var (isCorrect, _) = PostfixCalculator.Calculate("1 2 ` 3 *", stack);
        Assert.IsFalse(isCorrect);
    }

    private static IEnumerable<TestCaseData> Stacks
    => new TestCaseData[]
    {
        new TestCaseData(new StackWithArray()),
        new TestCaseData(new StackList()),
    };
}