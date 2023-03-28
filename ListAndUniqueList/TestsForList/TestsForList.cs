namespace TestList;

using ListAndUniqueList;

public class Tests
{
    List list;

    [SetUp]
    public void Setup()
    {
        list = new List();
    }

    [TestCaseSource(nameof(ListForTest))]
    public void ListShouldBeEmptyWhenCreated(List list)
    {
        Assert.True(list.IsEmpty());
    }

    [TestCaseSource(nameof(ListForTest))]
    public void WhenAddedToTheListElementListShouldNotBeEmpty(List list)
    {
        list.AddElement(1);
        Assert.False(list.IsEmpty());
    }

    [TestCaseSource(nameof(ListForTest))]
    public void WhenAddingAnElementToTheListAndThenDeletingItTheSheetShouldbeEmpty(List list)
    {
        list.AddElement(1);
        int item = 0;
        list.RemoveElement(ref item);
        Assert.True(list.IsEmpty());
    }

    [TestCaseSource(nameof(ListForTest))]
    public void TheListShouldReturnTheCorrectDataAfterDeletion(List list)
    {
        list.AddElement(1);
        list.AddElement(10);
        int item = 0;
        list.RemoveElement(ref item);
        Assert.True(item == 10);
    }

    [TestCaseSource(nameof(ListForTest))]
    public void TheListShouldCorrectlyReplaceThePositionData(List list)
    {
        list.AddElement(1);
        list.AddElement(10);
        list.ChangeValueByPosition(1, 15);
        int item = 0;
        list.RemoveElement(ref item);
        Assert.True(item == 15);
    }

    [TestCaseSource(nameof(ListForTest))]
    public void AnEmptyListShouldThrowAnExceptionWhenTryingToDelete(List list)
    {
        int item = 0;
        Assert.Throws<NullPointerException>(() => list.RemoveElement(ref item));
    }

    [TestCaseSource(nameof(ListForTest))]
    public void AnEmptyListShouldThrowAnExceptionWhenTryingToChangeValueByPosition(List list)
    {
        Assert.Throws<NullPointerException>(() => list.ChangeValueByPosition(10, 10));
    }

    [TestCaseSource(nameof(ListForTest))]
    public void AnEmptyListShouldThrowAnExceptionWhenTryingToChangeCheckValueByPosition(List list)
    {
        Assert.Throws<NullPointerException>(() => list.ReturnValueByPosition(10));
    }

    [TestCaseSource(nameof(ListForTest))]
    public void TheListShouldThrowAnExceptionIfThereIsNoPositionInTheListWhenReturningAnItemByPosition(List list)
    {
        list.AddElement(1);
        Assert.Throws<InvalidPositionException>(() => list.ReturnValueByPosition(10));
    }

    [TestCaseSource(nameof(ListForTest))]
    public void TheListShouldReturnTheCorrectItemByPosition(List list)
    {
        list.AddElement(15);
        int item = list.ReturnValueByPosition(0);
        Assert.True(item == 15);
    }

    private static IEnumerable<TestCaseData> ListForTest
    => new TestCaseData[]
    {
        new TestCaseData(new List()),
    };
}