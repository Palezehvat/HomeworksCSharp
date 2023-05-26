namespace TestsForStackCalculator;

using StackCalculator;
using System.Numerics;

public class Tests
{
    private const double delta = 0.0000000000001;


    [TestCaseSource(nameof(Stacks))]
    public void TheCalculatorShouldWorkCorrectlyToReturnTheCorrectValueOnASimpleExample(IStack stack)
    {
        var (isCorrect, number) = PostfixCalculator.Calculate("1 2 +", stack);
        Assert.True(isCorrect);
        Assert.That(number, Is.EqualTo(3));
    }

    [TestCaseSource(nameof(Stacks))]
    public void OnTheWrongLineTheCalculatorShouldReturnAnError(IStack stack)
    {
        var (isCorrect, _) = PostfixCalculator.Calculate("1 2", stack);
        Assert.IsFalse(isCorrect);
    }

    [TestCaseSource(nameof(Stacks))]
    public void TheFirstValueIsPositiveTheSecondIsNegativeTheCalculatorShouldReturnTheCorrectValue(IStack stack)
    {
        var (isCorrect, number) = PostfixCalculator.Calculate("1 -2 +", stack);
        Assert.That(isCorrect);
        Assert.That(number, Is.EqualTo(-1));
    }

    [TestCaseSource(nameof(Stacks))]
    public void TheSecondValueIsPositiveTheFirstIsNegativeTheCalculatorShouldReturnTheCorrectValue(IStack stack)
    {
        var (isCorrect, number) = PostfixCalculator.Calculate("-1 2 +", stack);
        Assert.That(isCorrect);
        Assert.That(number, Is.EqualTo(1));
    }

    [TestCaseSource(nameof(Stacks))]
    public void TheSecondValueIsNegativeTheFirstIsNegativeTheCalculatorShouldReturnTheCorrectValue(IStack stack)
    {
        var (isCorrect, number) = PostfixCalculator.Calculate("-1 -2 +", stack);
        Assert.That(isCorrect);
        Assert.That(number, Is.EqualTo(-3));
    }

    [TestCaseSource(nameof(Stacks))]
    public void TheStackCalculatorShouldWorkCorrectlyWithTheDifference(IStack stack)
    {
        var (isCorrect, number) = PostfixCalculator.Calculate("1 2 -", stack);
        Assert.That(isCorrect);
        Assert.That(number, Is.EqualTo(-1));
    }

    [TestCaseSource(nameof(Stacks))]
    public void TheStackCalculatorShouldWorkCorrectlyWithTheMultiplication(IStack stack)
    {
        var (isCorrect, number) = PostfixCalculator.Calculate("2 3 *", stack);
        Assert.That(isCorrect);
        Assert.That(number, Is.EqualTo(6));
    }

    [TestCaseSource(nameof(Stacks))]
    public void TheStackCalculatorShouldWorkCorrectlyWithTheDivision(IStack stack)
    {
        var (isCorrect, number) = PostfixCalculator.Calculate("5 2 /", stack);
        Assert.That(isCorrect);
        Assert.That(Math.Abs(number - 2.5) < delta);
    }

    [TestCaseSource(nameof(Stacks))]
    public void TheStackCalculatorShouldGiveAnErrorWhenReceivingAnemptyString(IStack stack)
    {
        var (isCorrect, _) = PostfixCalculator.Calculate("", stack);
        Assert.IsFalse(isCorrect);
    }

    [TestCaseSource(nameof(Stacks))]
    public void TheStackÑalculatorShouldCorrectlyCalculateComplexExpressions(IStack stack)
    {
        var (isCorrect, number) = PostfixCalculator.Calculate("1 2 + 3 *", stack);
        Assert.That(isCorrect);
        Assert.That(number, Is.EqualTo(9));
    }

    [TestCaseSource(nameof(Stacks))]
    public void TheStackCalculatorShouldGiveAnErrorWhenReceivingAStringWithCharactersThatWereNotExpected(IStack stack)
    {
        var (isCorrect, _) = PostfixCalculator.Calculate("1 2 ` 3 *", stack);
        Assert.IsFalse(isCorrect);
    }

    private static IEnumerable<TestCaseData> Stacks
    => new TestCaseData[]
    {
        new TestCaseData(new StackWithArray()),
        new TestCaseData(new StackWithList()),
    };
}