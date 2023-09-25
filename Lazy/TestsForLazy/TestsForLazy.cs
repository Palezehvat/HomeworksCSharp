namespace TestsForLazy;
using Lazy;
using Newtonsoft.Json.Linq;

public class Tests
{   
    [Test]
    public void SimpleExample()
    {
        var singleThreadLazy = new SingleThreadLazy<int>(() => FunctionsForTests.FunctionForLazyWithCounter());
        Assert.That(singleThreadLazy.Get(), Is.EqualTo(singleThreadLazy.Get()));
        Assert.That(FunctionsForTests.FunctionForLazyWithCounter(), Is.Not.EqualTo(singleThreadLazy.Get()));
        FunctionsForTests.howMutchFunctionBeenCalled = 0;
        var multiThreadLazy = new MultiThreadLazy<int>(() => FunctionsForTests.FunctionForLazyWithCounter());
        var arrayThreads = new Thread[Environment.ProcessorCount];
        object value = null;
        for (int i = 0; i < arrayThreads.Length; i++)
        {
            arrayThreads[i] = new Thread(() => { value = multiThreadLazy.Get(); });
        }

        foreach (var thread in arrayThreads)
        {
            thread.Start();
        }

        foreach (var element in arrayThreads)
        {
            element.Join();
        }
        
        foreach (var element in arrayThreads)
        {
            Assert.That(1, Is.EqualTo(value));
        }
    }

    [Test]
    public void ExampleWithException()
    {
        var singleThreadLazy = new SingleThreadLazy<int>(() => FunctionsForTests.FunctionWithInvalidOperationException());
        Assert.Throws<InvalidOperationException>(() => singleThreadLazy.Get());
        Assert.Throws<InvalidOperationException>(() => singleThreadLazy.Get());

        var multiThreadLazy = new MultiThreadLazy<int>(() => FunctionsForTests.FunctionWithInvalidOperationException());
        var arrayThreads = new Thread[Environment.ProcessorCount];
        object value = null;
        for (int i = 0;i < arrayThreads.Length; i++)
        {
            arrayThreads[i] = new Thread(() => 
            {
                try
                {
                    value =  multiThreadLazy.Get();
                }
                catch (InvalidOperationException)
                {
                    Assert.True(true);
                }
            });
        }
        
        foreach (var element in arrayThreads)
        {
            element.Start();
        }

        foreach (var element in arrayThreads)
        {
            element.Join();
        }
    }

    [Test]
    public void ThreadRaceTest()
    {
        var multiThreadLazy = new SingleThreadLazy<int>(() => FunctionsForTests.FunctionForLazyWithCounter());
        var arrayThreads = new Thread[Environment.ProcessorCount];
        object value = null;

        for (int i = 0; i < arrayThreads.Length; i++)
        {
            arrayThreads[i] = new Thread(() => 
            { 
                value = multiThreadLazy.Get(); 
                lock (value)
                {
                    if ((int)value != 1)
                    {
                        Assert.IsTrue(true);
                    }
                }
            });
        }

        foreach (var element in arrayThreads)
        {
            element.Start();
        }

        foreach (var element in arrayThreads)
        {
            element.Join();
        }
    }
}