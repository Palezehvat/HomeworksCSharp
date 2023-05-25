namespace TestsForMapFilterFold;

using MapFilterFold;
using System.Diagnostics;

public class Tests
{
    [Test]
    public void TheListOnIntShouldWorkCorrectlyWithFilter()
    {
        List<int> list = new List<int> { 1, 2, 3 };
        List<int> listCheck = new List<int> { 2 };
        list = FilterFoldMap.Filter(list, x => x % 2 == 0);
        Assert.IsTrue(list.SequenceEqual(listCheck));
    }

    [Test]
    public void TheListOnCharShouldWorkCorrectlyWithFilter()
    {
        List<char> list = new List<char> { '2', '3', '4' };
        List<char> listCheck = new List<char> { '2', '4' };
        list = FilterFoldMap.Filter(list, x => x % 2 == 0);
        Assert.IsTrue(list.SequenceEqual(listCheck));
    }

    [Test]
    public void AnEmptyListShouldFinishTheJobCorrectlyWithFilter()
    {
        List<int> list = null;
        Assert.Throws<NullReferenceException>(() => FilterFoldMap.Filter(list, x => x % 2 == 0));
    }

    [Test]
    public void TheListOnIntShouldWorkCorrectlyWithMap()
    {
        List<int> list = new List<int> { 1, 2, 3 };
        List<int> listCheck = new List<int> { 2, 4, 6 };
        list = FilterFoldMap.Map(list, x => x * 2);
        Assert.IsTrue(list.SequenceEqual(listCheck));
    }

    [Test]
    public void TheListOnCharShouldWorkCorrectlyWithMap()
    {
        List<char> list = new List<char> { '1', '2', '3' };
        List<char> listCheck = new List<char> { 'b', 'd', 'f' };
        list = FilterFoldMap.Map(list, x => (char)(x * 2));
        Assert.IsTrue(list.SequenceEqual(listCheck));
    }

    [Test]
    public void AnEmptyListShouldFinishTheJobCorrectlyWithMap()
    {
        List<int> list = null;
        Assert.Throws<NullReferenceException>(() => FilterFoldMap.Map(list, x => (x * 2)));
    }

    [Test]
    public void TheListOnIntShouldWorkCorrectlyWithFold()
    {
        List<int> list = new List<int> { 1, 2, 3 };
        var returnedNumber = FilterFoldMap.Fold(list, 1, (acc, elem) => acc * elem);
        Assert.That(6, Is.EqualTo(returnedNumber));
    }

    [Test]
    public void TheListOnCharShouldWorkCorrectlyWithFold()
    {
        List<char> list = new List<char> { '1', '2', '3' };
        var returnedNumber = FilterFoldMap.Fold(list, '1', (acc, elem) => (char)(acc * elem));
        Assert.That('氶', Is.EqualTo(returnedNumber));
    }

    [Test]
    public void AnEmptyListShouldFinishTheJobCorrectlyWithFold()
    {
        List<int> list = null;
        Assert.Throws<NullReferenceException>(()=> FilterFoldMap.Fold(list, 1, (acc, elem) => acc * elem));
    }
}