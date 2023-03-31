namespace TestsForStack;

using StackCalculator;

public class Tests
{
    [TestCaseSource(nameof(Stacks))]
    public void StackShouldNotEmptyAfterPush(Stack stack)
    {
        stack.AddElement(1);
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
        stack.AddElement(1);
        var (isCorrect, _) = stack.RemoveElement();
        Assert.IsTrue(isCorrect);
    }

    [TestCaseSource(nameof(Stacks))]
    public void StackShouldReturnTheLastValueThatWasAddedAndThenDeleted(Stack stack)
    {
        stack.AddElement(1);
        var (_, number) = stack.RemoveElement();
        Assert.IsTrue(number == 1);
    }

    [TestCaseSource(nameof(Stacks))]
    public void WhenRemovedFromTheTopOfTheStackTheElementShouldBeErasedFromTheTop(Stack stack)
    {
        stack.AddElement(1);
        var (_, _) = stack.RemoveElement();
        Assert.IsTrue(stack.IsEmpty());
    }

    [TestCaseSource(nameof(Stacks))]
    public void DeletingFromAnEmptyStackShouldCauseAnError(Stack stack)
    {
        var (isCorrect, _) = stack.RemoveElement();
        Assert.IsFalse(isCorrect);
    }

    [TestCaseSource(nameof(Stacks))]
    public void WhenAddingConsecutiveValuesTheTopValueShouldBeTheLastOneAdded(Stack stack)
    {
        stack.AddElement(1);
        stack.AddElement(2);
        var (_, number) = stack.RemoveElement();
        Assert.IsTrue(number == 2);
    }

    [TestCaseSource(nameof(Stacks))]
    public void DeletingInAStackOfMultipleItemsShouldBeSuccessful(Stack stack)
    {
        stack.AddElement(1);
        stack.AddElement(2);
        var (isCorrect, _) = stack.RemoveElement();
        Assert.IsTrue(isCorrect);
    }

    private static IEnumerable<TestCaseData> Stacks
    => new TestCaseData[]
    {
        new TestCaseData(new StackWithArray()),
        new TestCaseData(new StackList()),
    };
}