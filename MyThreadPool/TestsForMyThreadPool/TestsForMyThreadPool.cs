namespace TestsForMyThreadPool;

using MyThreadPool;

public class Tests
{
    [Test]
    public void ATestWithOneTaskForTenThreads()
    {
        var myThreadPool = new MyThreadPool(10);
        var task = myThreadPool.Submit(() => 2 * 2);
        Assert.That(4, Is.EqualTo(task.Result));
    }

    [Test]
    public void ATestWithTwoTasksForTenThreads()
    {
        var myThreadPool = new MyThreadPool(10);
        var firstTask = myThreadPool.Submit(() => 2 * 2);
        var secondTask = myThreadPool.Submit(() => 3 + 3);
        Assert.That(4, Is.EqualTo(firstTask.Result));
        Assert.That(6, Is.EqualTo(secondTask.Result));
    }

    [Test]
    public void WhenThereAreFewerThreadsThanTasks()
    {
        var myThreadPool = new MyThreadPool(1);
        var firstTask = myThreadPool.Submit(() => 2 * 2);
        var secondTask = myThreadPool.Submit(() => 3 + 3);
        Assert.That(4, Is.EqualTo(firstTask.Result));
        Assert.That(6, Is.EqualTo(secondTask.Result));
    }

    [Test]
    public void IsShutdownWorkingCorrectlyWithOneThread()
    {
        var token = new CancellationToken();
        var myThreadPool = new MyThreadPool(1);
        var firstTask = myThreadPool.Submit(() => 2 * 2);
        var secondTask = myThreadPool.Submit(() => 3 + 3);
        token.ThrowIfCancellationRequested();
        myThreadPool.Shutdown(token);
        Assert.That(4, Is.EqualTo(firstTask.Result));
        Assert.That(6, Is.EqualTo(secondTask.Result));
    }

    [Test]
    public void IsShutdownWorkingCorrectlyWithMultipleThread()
    {
        var token = new CancellationToken();
        var myThreadPool = new MyThreadPool(3);
        var firstTask = myThreadPool.Submit(() => 2 * 2);
        var secondTask = myThreadPool.Submit(() => 3 + 3);
        token.ThrowIfCancellationRequested();
        myThreadPool.Shutdown(token);
        Assert.That(4, Is.EqualTo(firstTask.Result));
        Assert.That(6, Is.EqualTo(secondTask.Result));
    }

    [Test]
    public void IsContinueWithWorkingCorrectlyWithWithOneSubtask()
    {
        var myThreadPool = new MyThreadPool(3);
        var myTask = myThreadPool.Submit(() => 2 * 2).ContinueWith(x => x.ToString());
        Assert.That("4", Is.EqualTo(myTask.Result));
    }

    [Test]
    public void IsContinueWithWorkingCorrectlyWithWithSeveralSubtask()
    {
        var myThreadPool = new MyThreadPool(3);
        var myTask = myThreadPool.Submit(() => 2 * 2).ContinueWith(x => x * x).ContinueWith(x => x.ToString());
        Assert.That("16", Is.EqualTo(myTask.Result));
    }
}