namespace TestsForStack;

using StackCalculator;

public class Tests
{
    [TestCaseSource(nameof(Stacks))]
    public void StackShouldNotEmptyAfterPush(Stack stack)
    {
        stack.Push(1);
        Assert.IsFalse(stack.IsEmpty());
    }

    [TestCaseSource(nameof(Stacks))]
    public void StackShouldEmptyWhenCreated(Stack stack)
    {
        Assert.IsTrue(stack.IsEmpty());
    }

    [TestCaseSource(nameof(Stacks))]
    public void StackShouldCorrectlyDeleteTheValue(Stack stack)
    {
        stack.Push(1);
        var (isCorrect, _) = stack.Pop();
        Assert.IsTrue(isCorrect);
    }

    [TestCaseSource(nameof(Stacks))]
    public void StackShouldReturnTheLastValueThatWasAddedAndThenDeleted(Stack stack)
    {
        stack.Push(1);
        var (_, number) = stack.Pop();
        Assert.That(1, Is.EqualTo(number));
    }

    [TestCaseSource(nameof(Stacks))]
    public void WhenRemovedFromTheTopOfTheStackTheElementShouldBeErasedFromTheTop(Stack stack)
    {
        stack.Push(1);
        var (_, _) = stack.Pop();
        Assert.IsTrue(stack.IsEmpty());
    }

    [TestCaseSource(nameof(Stacks))]
    public void DeletingFromAnEmptyStackShouldCauseAnError(Stack stack)
    {
        var (isCorrect, _) = stack.Pop();
        Assert.IsFalse(isCorrect);
    }

    [TestCaseSource(nameof(Stacks))]
    public void WhenAddingConsecutiveValuesTheTopValueShouldBeTheLastOneAdded(Stack stack)
    {
        stack.Push(1);
        stack.Push(2);
        var (_, number) = stack.Pop();
        Assert.That(2, Is.EqualTo(number));
    }

    [TestCaseSource(nameof(Stacks))]
    public void DeletingInAStackOfMultipleItemsShouldBeSuccessful(Stack stack)
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
        new TestCaseData(new StackList()),
    };
}