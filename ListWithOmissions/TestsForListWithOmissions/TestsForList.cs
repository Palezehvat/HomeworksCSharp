namespace TestsForListWithOmissions;

using ListWithOmissions;

public class Tests
{
    ListWithOmissions<int> list;
    [SetUp]
    public void Setup()
    {
        list = new ListWithOmissions<int>();
    }

    [Test]
    public void ListWithOmissionsShouldCorrectAdd()
    {
        list.Add(1);
        list.Add(2);
        list.Add(3);
        var listForCheck = new List<int> { 1, 2, 3 };
        int i = 0;
        foreach (var item in list)
        {
            if (item != listForCheck[i])
            {
                Assert.Fail();
            }
            ++i;
        }
        Assert.Pass();
    }
}