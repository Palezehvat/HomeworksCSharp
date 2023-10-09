namespace MyThreadPool;

/// <summary>
/// A class for automatic efficient flow control in the program.
/// </summary>
public class MyThreadPool
{
    private readonly MyThread[]? arrayThreads;
    private readonly Queue<Action> tasks = new();
    private readonly CancellationTokenSource token = new();
    private readonly Object lockerForThreads;
    private readonly Object lockerForTasks;
    private volatile bool stopCount = false;

    /// <summary>
    /// Constructor for creating n number of threads for tasks
    /// </summary>
    public MyThreadPool(int sizeThreads)
    {
        lockerForThreads = new();
        lockerForTasks = new();
        if (sizeThreads <= 0)
        {
            throw new ArgumentOutOfRangeException();
        }
        arrayThreads = new MyThread[sizeThreads];
        for (int i = 0; i < sizeThreads; i++)
        {
            arrayThreads[i] = new(tasks, token, lockerForThreads);
        }
    }

    /// <summary>
    /// Accepts a function, adds it as a task in the thread, and returns the created task
    /// </summary>
    public IMyTask<TResult> Submit<TResult>(Func<TResult> suppiler)
    {
        if (arrayThreads == null)
        {
            throw new ArgumentNullException();
        }
        var newTask = new MyTask<TResult>(suppiler, arrayThreads, tasks, lockerForTasks, token);
        tasks.Enqueue(() => newTask.StartSuppiler());
        return newTask;
    }

    /// <summary>
    /// Interrupts the processing of tasks that are not started do not begin, and those that are started are being completed
    /// </summary>
    public void Shutdown()
    {
        if (arrayThreads == null)
        {
            throw new ArgumentNullException(nameof(arrayThreads));
        }
        token.Cancel();
        
        if (token.IsCancellationRequested)
        {
            ;
        }

        for (int i = 0; i < arrayThreads.Length; ++i)
        {
            while (arrayThreads[i].GetIsAlive()) {}
        }

        foreach(var thread in arrayThreads)
        {
            thread.Join();
        }
        stopCount = true;
    }
}