namespace TestsForMyThreadPool;

using MyThreadPool;

public class Tests
{
    [Test]
    public void ATestWithOneTaskForTenThreads()
    {
        var myThreadPool = new MyThreadPool(10);
        var task = myThreadPool.Submit(() => 2 * 2);
        Assert.That(task.Result, Is.EqualTo(4));
        myThreadPool.Shutdown();
    }

    [Test]
    public void ATestWithTwoTasksForTenThreads()
    {
        var myThreadPool = new MyThreadPool(10);
        var firstTask = myThreadPool.Submit(() => 2 * 2);
        var secondTask = myThreadPool.Submit(() => 3 + 3);
        Assert.That(firstTask.Result, Is.EqualTo(4));
        Assert.That(secondTask.Result, Is.EqualTo(6));
        myThreadPool.Shutdown();
    }

    [Test]
    public void WhenThereAreFewerThreadsThanTasks()
    {
        var myThreadPool = new MyThreadPool(1);
        var firstTask = myThreadPool.Submit(() => 2 * 2);
        var secondTask = myThreadPool.Submit(() => 3 + 3);
        Assert.That(firstTask.Result, Is.EqualTo(4));
        Assert.That(secondTask.Result, Is.EqualTo(6));
        myThreadPool.Shutdown();
    }
    
    [Test]
    public void IsContinueWithWorkingCorrectlyWithWithOneSubtask()
    {
        var myThreadPool = new MyThreadPool(3);
        var myTask = myThreadPool.Submit(() => 2 * 2).ContinueWith(x => x.ToString());
        Assert.That(myTask.Result, Is.EqualTo("4"));
        myThreadPool.Shutdown();
    }
    
    [Test]
    public void IsContinueWithWorkingCorrectlyWithWithSeveralSubtask()
    {
        var myThreadPool = new MyThreadPool(3);
        var myTask = myThreadPool.Submit(() => 2 * 2).ContinueWith(x => x * x).ContinueWith(x => x.ToString());
        Assert.That(myTask.Result, Is.EqualTo("16"));
        myThreadPool.Shutdown();
    }

    [Test]
    public void NumberThreadsTheRequiredNumberIsCreated()
    {
        var myThreadPool = new MyThreadPool(5);
        Assert.That(myThreadPool.GetNumberOfThreads(), Is.EqualTo(5));
        myThreadPool.Shutdown();
    }

    [Test]
    public void WorkingWithException()
    {
        var myThreadPool = new MyThreadPool(10);
        var task = myThreadPool.Submit(() =>
        {
            int zero = 0;
            return 1 / zero;
        });
        Assert.Throws<AggregateException>(() => { var result = task.Result;});
        myThreadPool.Shutdown();
    }

    [Test]
    public void TheTaskWillCompletedWhenExceptionResult()
    {
        var myThreadPool = new MyThreadPool(10);
        var task = myThreadPool.Submit(() =>
        {
            int zero = 0;
            return 1 / zero;
        });
        Assert.Throws<AggregateException>(() => { var result = task.Result; });
        Assert.That(task.IsCompleted, Is.True);
        myThreadPool.Shutdown();
    }

    [Test]
    public void AnotherTasksWillNotBeAcceptedAndTheOldOnesWillBeFinalized()
    {
        var myThreadPool = new MyThreadPool(1);
        var firstTask = myThreadPool.Submit(() => 2 * 2);
        myThreadPool.Shutdown();
        Assert.That(firstTask.Result, Is.EqualTo(4));
        Assert.Throws<ShudownWasThrownException>(() => { var secondTask = myThreadPool.Submit(() => 3 + 3); });
    }
}