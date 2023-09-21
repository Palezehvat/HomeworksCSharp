namespace TestsForLazy;
using Lazy;

public class Tests
{
    private static Func<string> function = () => DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss:fff");
    
    [TestCaseSource(nameof(Lazys))]
    public void SimpleExample(ILazy<string> lazy)
    {
        Assert.That(lazy.Get(), Is.EqualTo(lazy.Get()));
    }

    [Test]
    public void ExampleWithException()
    {
        var singleThreadLazy = new SingleThreadLazy<int>(() => throw new InvalidOperationException());
        var multiThreadLazy = new MultiThreadLazy<int>(() => throw new InvalidOperationException());
        Assert.Throws<InvalidOperationException>(() => singleThreadLazy.Get());
        Assert.Throws<InvalidOperationException>(() => singleThreadLazy.Get());
        Assert.Throws<InvalidOperationException>(() => multiThreadLazy.Get());
        Assert.Throws<InvalidOperationException>(() => multiThreadLazy.Get());
    }

    private static IEnumerable<TestCaseData> Lazys
    => new TestCaseData[]
    {
        new TestCaseData(new SingleThreadLazy<string>(function)),
        new TestCaseData(new MultiThreadLazy<string>(function)),
    };
}