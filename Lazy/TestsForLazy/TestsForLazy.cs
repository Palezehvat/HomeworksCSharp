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

        var arrayResult = new int[10];

        for (int i = 0; i < arrayThreads.Length; i++)
        {
            var local = i;
            arrayThreads[i] = new Thread(() => 
            {
                for (var j = local; j < 10; j++)
                {
                    arrayResult[j] = multiThreadLazy.Get();
                }
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

        foreach (var element in arrayResult)
        {
            Assert.That(Equals(element, 1));
        }
    }

    [TestCaseSource(nameof(LazyForTestWithFunctionWithCounter))]
    public void SimpleExampleWithOneThread(ILazy<int> lazy)
    {
        for (int i = 0; i < 10; ++i)
        {
            Assert.That(lazy.Get(), Is.EqualTo(1));
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
        var arrayResult = new int[10];

        for (int i = 0; i < arrayThreads.Length; i++)
        {
            var local = i;
            arrayThreads[i] = new Thread(() => 
            {
                for (var j = local; j < 10; j++)
                {
                    arrayResult[j] = multiThreadLazy.Get();
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

        foreach (var element in arrayResult)
        {
            Assert.That(Equals(element, 1));
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