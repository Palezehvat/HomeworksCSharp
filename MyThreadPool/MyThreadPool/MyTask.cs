namespace MyThreadPool;

/// <summary>
/// A class for creating tasks
/// </summary>
public class MyTask<TResult> : IMyTask<TResult>
{
    private Func<TResult>? suppiler;
    private volatile bool isCompleted = false;
    private TResult? result;
    private Queue<Action> queueWithContinueWithTasks;
    private MyThread[] arrayThreads;
    private Queue<Action> queueWithTasks;
    private Object locker;
    private volatile bool stopCount;

    /// <summary>
    /// Constructor for creating a task
    /// </summary>
    public MyTask(Func<TResult> suppiler, MyThread[] arrayThreads, Queue<Action> queueWithTasks, Object locker, bool stopCount)
    {
        this.suppiler = suppiler;
        queueWithContinueWithTasks = new ();
        this.arrayThreads = arrayThreads;
        this.queueWithTasks = queueWithTasks;
        this.locker = locker;
        this.stopCount = stopCount;
    }

    public TResult? Result
    {
        get
        {
            while (!isCompleted && !stopCount) {}
            return result;
        }
    }
    public bool IsCompleted
    {
        get
        {
            return isCompleted;
        }
    }

    /// <summary>
    /// Task completion
    /// </summary>
    public void StartSuppiler()
    {
        if (suppiler != null)
        {
            result = suppiler();
            isCompleted = true;
            while (queueWithContinueWithTasks.Count > 0)
            {
                queueWithTasks.Enqueue(queueWithContinueWithTasks.Dequeue());
            }
        }
    }

    /// <summary>
    /// Checking that the task queue is empty
    /// </summary>
    public bool IsQueueEmpty()
    { 
        return queueWithContinueWithTasks.Count == 0;
    }

    public IMyTask<TNewResult> ContinueWith<TNewResult>(Func<TResult, TNewResult> suppiler)
    {
        var newTask = new MyTask<TNewResult>(() => suppiler(Result), arrayThreads, queueWithTasks, locker, stopCount);
        lock(locker)
        {
            if (IsCompleted)
            {
                queueWithTasks.Enqueue(() => newTask.StartSuppiler());
            }
            else
            {
                queueWithContinueWithTasks.Enqueue(() => newTask.StartSuppiler());
            }
        }
        return newTask;
    }
}