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
        uniqueList.AddElement(2, 15);
        Assert.Throws<InvalidPositionException>(() => uniqueList.ReturnValueByPosition(2));
    }

    [Test]
    public void UniqueListShouldThrowAnExceptionWhenReceivingARepeatingValueWhenChangingValue()
    {
        uniqueList.AddElement(0, 15);
        uniqueList.AddElement(1, 10);

        Assert.That(uniqueList.ReturnValueByPosition(1), Is.EqualTo(10));
    }
}