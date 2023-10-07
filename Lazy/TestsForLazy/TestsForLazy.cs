namespace TestsForLazy;
using Lazy;

public class Tests
{
    private static readonly FunctionsForTests globalFunctionsForTestsForSingleThread = new();
    private static readonly FunctionsForTests globalFunctionsForTestsForMultiThread = new();

    [Test]
    public void SimpleExampleWithMultiThreadLazy()
    {
        var functionsForTests = new FunctionsForTests();
        var multiThreadLazy = new MultiThreadLazy<int>(() => functionsForTests.FunctionForLazyWithCounter());
        var arrayThreads = new Thread[10];
        for (int i = 0; i < arrayThreads.Length; i++)
        {
            arrayThreads[i] = new Thread(() => 
            {
                Assert.That(Equals(multiThreadLazy.Get(), 1));
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

    [TestCaseSource(nameof(LazyForTestWithFunctionWithCounter))]
    public void SimpleExampleWithOneThread(ILazy<int> lazy)
    {
        for (int i = 0; i < 10; ++i)
        {
            Assert.That(Equals(lazy.Get(), 1));
        }
    }

    [TestCaseSource(nameof(LazyForTestWithFunctionWithException))]
    public void ExampleWithExceptionWithOneThread(ILazy<int> lazy)
    {
        for (int i = 0; i < 10; ++i)
        {
            Assert.Throws<DivideByZeroException>(() => lazy.Get());
        }
    }

    [Test]
    public void ThreadRaceTest()
    {
        var functionsForTests = new FunctionsForTests();
        var multiThreadLazy = new MultiThreadLazy<int>(() => functionsForTests.FunctionForLazyWithCounter());
        var arrayThreads = new Thread[10];

        for (int i = 0; i < arrayThreads.Length; i++)
        {
            arrayThreads[i] = new Thread(() => 
            { 
                Assert.That(Equals(multiThreadLazy.Get(), 1)); 
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