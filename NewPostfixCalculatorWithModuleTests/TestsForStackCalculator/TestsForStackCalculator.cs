namespace TestsForStackCalculator;

using StackCalculator;
using System.Numerics;

public class Tests
{
    private const double delta = 0.0000000000001;
    PostfixCalculator calculator;
    [SetUp]
    public void Setup()
    {
        calculator= new PostfixCalculator();
    }

    [TestCaseSource(nameof(Stacks))]
    public void TheCalculatorShouldWorkCorrectlyToReturnTheCorrectValueOnASimpleExample(Stack stack)
    {
        Setup();
        var (isCorrect, number) = calculator.ConvertToAResponse("1 2 +", stack);
        Assert.IsTrue(isCorrect && number == 3);
    }

    [TestCaseSource(nameof(Stacks))]
    public void OnTheWrongLineTheCalculatorShouldReturnAnError(Stack stack)
    {
        Setup();
        var (isCorrect, _) = calculator.ConvertToAResponse("1 2", stack);
        Assert.IsFalse(isCorrect);
    }

    [TestCaseSource(nameof(Stacks))]
    public void TheFirstValueIsPositiveTheSecondIsNegativeTheCalculatorShouldReturnTheCorrectValue(Stack stack)
    {
        Setup();
        var (isCorrect, number) = calculator.ConvertToAResponse("1 -2 +", stack);
        Assert.IsTrue(isCorrect && number == -1);
    }

    [TestCaseSource(nameof(Stacks))]
    public void TheSecondValueIsPositiveTheFirstIsNegativeTheCalculatorShouldReturnTheCorrectValue(Stack stack)
    {
        Setup();
        var (isCorrect, number) = calculator.ConvertToAResponse("-1 2 +", stack);
        Assert.IsTrue(isCorrect && number == 1);
    }

    [TestCaseSource(nameof(Stacks))]
    public void TheSecondValueIsNegativeTheFirstIsNegativeTheCalculatorShouldReturnTheCorrectValue(Stack stack)
    {
        Setup();
        var (isCorrect, number) = calculator.ConvertToAResponse("-1 -2 +", stack);
        Assert.IsTrue(isCorrect && number == -3);
    }

    [TestCaseSource(nameof(Stacks))]
    public void TheStackCalculatorShouldWorkCorrectlyWithTheDifference(Stack stack)
    {
        Setup();
        var (isCorrect, number) = calculator.ConvertToAResponse("1 2 -", stack);
        Assert.IsTrue(isCorrect && number == -1);
    }

    [TestCaseSource(nameof(Stacks))]
    public void TheStackCalculatorShouldWorkCorrectlyWithTheMultiplication(Stack stack)
    {
        Setup();
        var (isCorrect, number) = calculator.ConvertToAResponse("2 3 *", stack);
        Assert.IsTrue(isCorrect && number == 6);
    }

    [TestCaseSource(nameof(Stacks))]
    public void TheStackCalculatorShouldWorkCorrectlyWithTheDivision(Stack stack)
    {
        Setup();
        var (isCorrect, number) = calculator.ConvertToAResponse("5 2 /", stack);
        Assert.IsTrue(isCorrect && Math.Abs(number - 2.5) < delta);
    }

    [TestCaseSource(nameof(Stacks))]
    public void TheStackCalculatorShouldGiveAnErrorWhenReceivingAnemptyString (Stack stack)
    {
        Setup();
        var (isCorrect, _) = calculator.ConvertToAResponse("", stack);
        Assert.IsFalse(isCorrect);
    }

    [TestCaseSource(nameof(Stacks))]
    public void TheStackÑalculatorShouldCorrectlyCalculateComplexExpressions(Stack stack)
    {
        Setup();
        var (isCorrect, number) = calculator.ConvertToAResponse("1 2 + 3 *", stack);
        Assert.IsTrue(isCorrect && number == 9);
    }

    [TestCaseSource(nameof(Stacks))]
    public void TheStackCalculatorShouldGiveAnErrorWhenReceivingAStringWithCharactersThatWereNotExpected(Stack stack)
    {
        Setup();
        var (isCorrect, _) = calculator.ConvertToAResponse("1 2 ` 3 *", stack);
        Assert.IsFalse(isCorrect);
    }

    private static IEnumerable<TestCaseData> Stacks
    => new TestCaseData[]
    {
        new TestCaseData(new StackWithArray()),
        new TestCaseData(new StackList()),
    };
}