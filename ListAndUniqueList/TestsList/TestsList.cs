namespace TestList;

using ListAndUniqueList;

public class Tests
{
    [TestCaseSource(nameof(ListForTest))]
    public void ListShouldBeEmptyWhenCreated(List list)
    {
        Assert.True(list.IsEmpty());
    }

    [TestCaseSource(nameof(ListForTest))]
    public void WhenAddedToTheListElementListShouldNotBeEmpty(List list)
    {
        list.AddElement(0, 1);
        Assert.False(list.IsEmpty());
    }

    [TestCaseSource(nameof(ListForTest))]
    public void WhenAddingAnElementToTheListAndThenDeletingItTheSheetShouldbeEmpty(List list)
    {
        list.AddElement(0, 1);
        int item = 0;
        list.RemoveElement(item);
        Assert.True(list.IsEmpty());
    }

    [TestCaseSource(nameof(ListForTest))]
    public void TheListShouldReturnTheCorrectDataAfterDeletion(List list)
    {
        list.AddElement(0, 1);
        list.AddElement(1, 10);
        list.AddElement(2, 12);
        int item = list.RemoveElement(1);
        Assert.That(10, Is.EqualTo(item));
    }

    [TestCaseSource(nameof(ListForTest))]
    public void TheListShouldCorrectlyReplaceThePositionData(List list)
    {
        list.AddElement(0, 1);
        list.AddElement(1, 10);
        list.ChangeValueByPosition(1, 15);
        int item = list.RemoveElement(1);
        Assert.That(15, Is.EqualTo(item));
    }

    [TestCaseSource(nameof(ListForTest))]
    public void AnEmptyListShouldThrowAnExceptionWhenTryingToDelete(List list)
    {
        Assert.Throws<NullListException>(() => list.RemoveElement(0));
    }

    [TestCaseSource(nameof(ListForTest))]
    public void AnEmptyListShouldThrowAnExceptionWhenTryingToChangeValueByPosition(List list)
    {
        Assert.Throws<NullListException>(() => list.ChangeValueByPosition(10, 10));
    }

    [TestCaseSource(nameof(ListForTest))]
    public void AnEmptyListShouldThrowAnExceptionWhenTryingToChangeCheckValueByPosition(List list)
    {
        Assert.Throws<NullListException>(() => list.ReturnValueByPosition(10));
    }

    [TestCaseSource(nameof(ListForTest))]
    public void TheListShouldThrowAnExceptionIfThereIsNoPositionInTheListWhenReturningAnItemByPosition(List list)
    {
        list.AddElement(0, 1);
        Assert.Throws<InvalidPositionException>(() => list.ReturnValueByPosition(10));
    }

    [TestCaseSource(nameof(ListForTest))]
    public void TheListShouldReturnTheCorrectItemByPosition(List list)
    {
        list.AddElement(0, 15);
        int item = list.ReturnValueByPosition(0);
        Assert.That(15, Is.EqualTo(item));
    }

    private static IEnumerable<TestCaseData> ListForTest
    => new TestCaseData[]
    {
        new TestCaseData(new List()),
        new TestCaseData(new UniqueList()),
    };
}