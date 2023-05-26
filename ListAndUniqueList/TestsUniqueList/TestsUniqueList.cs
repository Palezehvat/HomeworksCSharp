namespace TestsForUniqueList;

using ListAndUniqueList;

public class Tests
{
    private UniqueList uniqueList;

    [SetUp]
    public void Setup()
    {
        uniqueList = new UniqueList();
    }

    [Test]
    public void UniqueListShouldThrowAnExceptionWhenReceivingARepeatingValueWhenAdding()
    {
        uniqueList.AddElement(0, 15);
        uniqueList.AddElement(1, 10);
        Assert.Throws<InvalidItemException>(() => uniqueList.AddElement(2, 15));
    }

    [Test]
    public void UniqueListShouldThrowAnExceptionWhenReceivingARepeatingValueWhenChangingValue()
    {
        uniqueList.AddElement(0, 15);
        uniqueList.AddElement(1, 10);

        Assert.That(uniqueList.ReturnValueByPosition(1), Is.EqualTo(10));
    }

    [Test]
    public void ChangeValueByPositionWhenValueWithChangeValueShouldWorkCorrectly()
    {
        uniqueList.AddElement(0, 15);
        uniqueList.AddElement(1, 10);
        uniqueList.ChangeValueByPosition(1, 10);
    }

    [Test]
    public void ChangeValueByPositionWhenValueWithChangeValueShouldThrowException()
    {
        uniqueList.AddElement(0, 15);
        uniqueList.AddElement(1, 10);
        uniqueList.ChangeValueByPosition(1, 10);
        Assert.Throws<InvalidItemException>(() => uniqueList.ChangeValueByPosition(0, 10));
    }
}