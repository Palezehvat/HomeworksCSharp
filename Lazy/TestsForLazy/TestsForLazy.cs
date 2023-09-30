namespace TestsForLazy;
using Lazy;

public class Tests
{
    private static FunctionsForTests globalFunctionsForTestsForSingleThread = new();
    private static FunctionsForTests globalFunctionsForTestsForMultiThread = new();

    [Test]
    public void SimpleExampleWithMultiThreadLazy()
    {
        var functionsForTests = new FunctionsForTests();
        var result = 1;
        var multiThreadLazy = new MultiThreadLazy<int>(() => functionsForTests.FunctionForLazyWithCounter());
        var arrayThreads = new Thread[10];
        for (int i = 0; i < arrayThreads.Length; i++)
        {
            arrayThreads[i] = new Thread(() => 
            {
                Assert.That(result, Is.EqualTo(multiThreadLazy.Get()));
            });
        }

        foreach (var thread in arrayThreads)
        {
            thread.Start();
        }

        foreach (var element in arrayThreads)
        {
            element.Join();
        }
    }

    [Test]
    public void ExampleWithExceptionWithMultiThreadLazy()
    {
        var functionsForTests = new FunctionsForTests();
        var multiThreadLazy = new MultiThreadLazy<int>(() => functionsForTests.FunctionWithInvalidOperationException());
        var arrayThreads = new Thread[10];
        object? value = null;
        for (int i = 0;i < arrayThreads.Length; i++)
        {
            arrayThreads[i] = new Thread(() => 
            {
                bool isExceptionWasThrown = false;
                try
                {
                    value =  multiThreadLazy.Get();
                }
                catch (InvalidOperationException)
                {
                    isExceptionWasThrown = true;
                }
                Assert.True(isExceptionWasThrown);
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

    [TestCaseSource(nameof(LazyForTestWithFunctionWithCounter))]
    public void SimpleExampleWithOneThread(ILazy<int> lazy)
    {
        var result = 1;
        for (int i = 0; i < 10; ++i)
        {
            Assert.That(result, Is.EqualTo(lazy.Get()));
        }
    }

    [TestCaseSource(nameof(LazyForTestWithFunctionWithException))]
    public void ExampleWithExceptionWithOneThread(ILazy<int> lazy)
    {
        for (int i = 0; i < 10; ++i)
        {
            Assert.Throws<InvalidOperationException>(() => lazy.Get());
        }
    }

    [Test]
    public void ThreadRaceTest()
    {
        var functionsForTests = new FunctionsForTests();
        var multiThreadLazy = new MultiThreadLazy<int>(() => functionsForTests.FunctionForLazyWithCounter());
        var arrayThreads = new Thread[10];

        var correctResult = 1;

        for (int i = 0; i < arrayThreads.Length; i++)
        {
            arrayThreads[i] = new Thread(() => 
            { 
                Assert.That(correctResult, Is.EqualTo(multiThreadLazy.Get())); 
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

    private static IEnumerable<TestCaseData> LazyForTestWithFunctionWithCounter
    => new TestCaseData[]
    {
        new TestCaseData(new MultiThreadLazy<int>(() => globalFunctionsForTestsForSingleThread.FunctionForLazyWithCounter())),
        new TestCaseData(new SingleThreadLazy<int>(() => globalFunctionsForTestsForMultiThread.FunctionForLazyWithCounter())),
    };

    private static IEnumerable<TestCaseData> LazyForTestWithFunctionWithException
    => new TestCaseData[]
    {
        new TestCaseData(new SingleThreadLazy<int>(() => globalFunctionsForTestsForSingleThread.FunctionWithInvalidOperationException())),
        new TestCaseData(new MultiThreadLazy<int>(() => globalFunctionsForTestsForMultiThread.FunctionWithInvalidOperationException())),
    };
}