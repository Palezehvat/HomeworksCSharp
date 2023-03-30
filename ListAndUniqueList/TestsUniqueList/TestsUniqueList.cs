namespace TestsForUniqueList;

using ListAndUniqueList;
using Newtonsoft.Json.Linq;

public class Tests
{
    UniqueList uniqueList;

    [SetUp]
    public void Setup()
    {
        uniqueList = new UniqueList();
    }

    [TestCaseSource(nameof(ListForTest))]
    public void UniqueListShouldBeEmptyWhenCreated(UniqueList uniqueList)
    {
        Assert.True(uniqueList.IsEmpty());
    }

    [TestCaseSource(nameof(ListForTest))]
    public void WhenAddedToTheListElementUniqueListShouldNotBeEmpty(UniqueList uniqueList)
    {
        uniqueList.AddElement(1);
        Assert.False(uniqueList.IsEmpty());
    }

    [TestCaseSource(nameof(ListForTest))]
    public void WhenAddingAnElementToTheUniqueListAndThenDeletingItTheSheetShouldbeEmpty(UniqueList uniqueList)
    {
        uniqueList.AddElement(1);
        int item = 0;
        uniqueList.RemoveElement(ref item);
        Assert.True(uniqueList.IsEmpty());
    }

    [TestCaseSource(nameof(ListForTest))]
    public void TheUniqueListShouldReturnTheCorrectDataAfterDeletion(UniqueList uniqueList)
    {
        uniqueList.AddElement(1);
        uniqueList.AddElement(10);
        int item = 0;
        uniqueList.RemoveElement(ref item);
        Assert.True(item == 10);
    }

    [TestCaseSource(nameof(ListForTest))]
    public void TheUniqueListShouldCorrectlyReplaceThePositionData(UniqueList uniqueList)
    {
        uniqueList.AddElement(1);
        uniqueList.AddElement(10);
        uniqueList.ChangeValueByPosition(1, 15);
        int item = 0;
        uniqueList.RemoveElement(ref item);
        Assert.True(item == 15);
    }

    [TestCaseSource(nameof(ListForTest))]
    public void AnEmptyUniqueListShouldThrowAnExceptionWhenTryingToDelete(UniqueList uniqueList)
    {
        int item = 0;
        Assert.Throws<NullPointerException>(() => uniqueList.RemoveElement(ref item));
    }

    [TestCaseSource(nameof(ListForTest))]
    public void AnEmptyUniqueListShouldThrowAnExceptionWhenTryingToChangeValueByPosition(UniqueList uniqueList)
    {
        Assert.Throws<NullPointerException>(() => uniqueList.ChangeValueByPosition(10, 10));
    }

    [TestCaseSource(nameof(ListForTest))]
    public void AnEmptyUniqueListShouldThrowAnExceptionWhenTryingToChangeCheckValueByPosition(UniqueList uniqueList)
    {
        Assert.Throws<NullPointerException>(() => uniqueList.ReturnValueByPosition(10));
    }

    [TestCaseSource(nameof(ListForTest))]
    public void TheUniqueListShouldThrowAnExceptionIfThereIsNoPositionInTheListWhenReturningAnItemByPosition(UniqueList uniqueList)
    {
        uniqueList.AddElement(1);
        Assert.Throws<InvalidPositionException>(() => uniqueList.ReturnValueByPosition(10));
    }

    [TestCaseSource(nameof(ListForTest))]
    public void TheUniqueListShouldReturnTheCorrectItemByPosition(UniqueList uniqueList)
    {
        uniqueList.AddElement(15);
        int item = uniqueList.ReturnValueByPosition(0);
        Assert.True(item == 15);
    }

    [TestCaseSource(nameof(ListForTest))]
    public void UniqueListShouldThrowAnExceptionWhenReceivingARepeatingValueWhenAdding(UniqueList uniqueList)
    {
        uniqueList.AddElement(15);
        uniqueList.AddElement(10);
        Assert.Throws<InvalidItemException>(() => uniqueList.AddElement(15));
    }

    [TestCaseSource(nameof(ListForTest))]
    public void UniqueListShouldThrowAnExceptionWhenReceivingARepeatingValueWhenChangingValue(UniqueList uniqueList)
    {
        uniqueList.AddElement(15);
        uniqueList.AddElement(10);
        Assert.Throws<InvalidItemException>(() => uniqueList.ChangeValueByPosition(1, 15));
    }

    private static IEnumerable<TestCaseData> ListForTest
    => new TestCaseData[]
    {
        new TestCaseData(new UniqueList()),
    };
}