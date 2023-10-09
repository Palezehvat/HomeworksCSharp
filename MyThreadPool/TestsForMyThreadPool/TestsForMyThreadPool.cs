namespace TestsForMyThreadPool;

using MyThreadPool;

public class Tests
{
    [Test]
    public void ATestWithOneTaskForTenThreads()
    {
        var myThreadPool = new MyThreadPool(10);
        var task = myThreadPool.Submit(() => 2 * 2);
        Assert.That(Equals(task.Result, 4));
        myThreadPool.Shutdown();
    }

    [Test]
    public void ATestWithTwoTasksForTenThreads()
    {
        var myThreadPool = new MyThreadPool(10);
        var firstTask = myThreadPool.Submit(() => 2 * 2);
        var secondTask = myThreadPool.Submit(() => 3 + 3);
        Assert.That(Equals(firstTask.Result, 4));
        Assert.That(Equals(secondTask.Result, 6));
        myThreadPool.Shutdown();
    }

    [Test]
    public void WhenThereAreFewerThreadsThanTasks()
    {
        var myThreadPool = new MyThreadPool(1);
        var firstTask = myThreadPool.Submit(() => 2 * 2);
        var secondTask = myThreadPool.Submit(() => 3 + 3);
        Assert.That(Equals(firstTask.Result, 4));
        Assert.That(Equals(secondTask.Result, 6));
        myThreadPool.Shutdown();
    }

    [Test]
    public void IsContinueWithWorkingCorrectlyWithWithOneSubtask()
    {
        var myThreadPool = new MyThreadPool(3);
        var myTask = myThreadPool.Submit(() => 2 * 2).ContinueWith(x => x.ToString());
        Assert.That(Equals(myTask.Result, "4"));
        myThreadPool.Shutdown();
    }

    [Test]
    public void IsContinueWithWorkingCorrectlyWithWithSeveralSubtask()
    {
        var myThreadPool = new MyThreadPool(3);
        var myTask = myThreadPool.Submit(() => 2 * 2).ContinueWith(x => x * x).ContinueWith(x => x.ToString());
        Assert.That(Equals(myTask.Result, "16"));
        myThreadPool.Shutdown();
    }
}