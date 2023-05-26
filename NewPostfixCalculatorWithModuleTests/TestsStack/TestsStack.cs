namespace TestsForStack;

using StackCalculator;

public class Tests
{
    [TestCaseSource(nameof(Stacks))]
    public void StackShouldNotEmptyAfterPush(IStack stack)
    {
        stack.Push(1);
        Assert.IsFalse(stack.IsEmpty());
    }

    [TestCaseSource(nameof(Stacks))]
    public void StackShouldEmptyWhenCreated(IStack stack)
    {
        Assert.IsTrue(stack.IsEmpty());
    }

    [TestCaseSource(nameof(Stacks))]
    public void StackShouldCorrectlyDeleteTheValue(IStack stack)
    {
        stack.Push(1);
        var (isCorrect, _) = stack.Pop();
        Assert.IsTrue(isCorrect);
    }

    [TestCaseSource(nameof(Stacks))]
    public void StackShouldReturnTheLastValueThatWasAddedAndThenDeleted(IStack stack)
    {
        stack.Push(1);
        var (_, number) = stack.Pop();
        Assert.That(number, Is.EqualTo(1));
    }

    [TestCaseSource(nameof(Stacks))]
    public void WhenRemovedFromTheTopOfTheStackTheElementShouldBeErasedFromTheTop(IStack stack)
    {
        stack.Push(1);
        var (_, _) = stack.Pop();
        Assert.IsTrue(stack.IsEmpty());
    }

    [TestCaseSource(nameof(Stacks))]
    public void DeletingFromAnEmptyStackShouldCauseAnError(IStack stack)
    {
        var (isCorrect, _) = stack.Pop();
        Assert.IsFalse(isCorrect);
    }

    [TestCaseSource(nameof(Stacks))]
    public void WhenAddingConsecutiveValuesTheTopValueShouldBeTheLastOneAdded(IStack stack)
    {
        stack.Push(1);
        stack.Push(2);
        var (_, number) = stack.Pop();
        Assert.That(number, Is.EqualTo(2));
    }

    [TestCaseSource(nameof(Stacks))]
    public void DeletingInAStackOfMultipleItemsShouldBeSuccessful(IStack stack)
    {
        stack.Push(1);
        stack.Push(2);
        var (isCorrect, _) = stack.Pop();
        Assert.IsTrue(isCorrect);
    }

    private static IEnumerable<TestCaseData> Stacks
    => new TestCaseData[]
    {
        new TestCaseData(new StackWithArray()),
        new TestCaseData(new StackWithList()),
    };
}