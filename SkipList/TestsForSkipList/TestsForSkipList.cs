namespace TestsForSkipList;

using SkipList;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Tests
{
    SkipList<int> list;
    [SetUp]
    public void Setup()
    {
        list = new SkipList<int>();
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

    [Test]
    public void AnUninitializedListShouldThrowExceptionWhenTryingToGetSize()
    {
        Assert.Throws<NullReferenceException>(() => list.Count());
    }

    [Test]
    public void NullListShouldThrowExceptionThenWhenTryingToCopyValuesToAnArray()
    {
        var arrayCopy = new int[5]; 
        Assert.Throws<NullReferenceException>(() => list.CopyTo(arrayCopy, 0));
    }

    [Test]
    public void NullListShouldThrowExceptionThenWhenTryingToRemoveValue()
    {
        Assert.Throws<NullReferenceException>(() => list.Remove(3));
    }

    [Test]
    public void NullListShouldThrowExceptionThenWhenTryingToRemoveByIndexValue()
    {
        Assert.Throws<NullReferenceException>(() => list.RemoveAt(3));
    }

    [Test]
    public void NullListShouldThrowExceptionThenWhenTryingToUseMethodContains()
    {
        Assert.Throws<NullReferenceException>(() => list.Contains(3));
    }

    
    [Test]
    public void ListWithMethodContainsShouldWorkCorrectly()
    {
        list.Add(3);
        list.Add(4);
        list.Add(5);
        Assert.True(list.Contains(4) && !list.Contains(7));
    }
    
    
    [Test]
    public void ListWithMethodCopyToShouldWorkCorrectly()
    {
        list.Add(3);
        list.Add(4);
        list.Add(5);
        var arrayToCopy = new int[3];
        var arrayCheck = new int[3]{3, 4, 5 };
        list.CopyTo(arrayToCopy, 0);
        Assert.True(arrayToCopy.SequenceEqual(arrayCheck));
    }
    
    [Test]
    public void ListWithMethodRemoveShouldWorkCorrectly()
    {
        list.Add(3);
        list.Add(4);
        list.Add(5);
        list.Remove(4);
        Assert.True(!list.Contains(4));
    }

    [Test]
    public void ListWithMethodRemoveAtShouldWorkCorrectly()
    {
        list.Add(3);
        list.Add(4);
        list.Add(5);
        list.RemoveAt(1);
        Assert.True(!list.Contains(4));
    }
}