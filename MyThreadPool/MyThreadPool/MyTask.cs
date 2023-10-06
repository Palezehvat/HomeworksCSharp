namespace MyThreadPool;

public class MyTask<TResult> : IMyTask<TResult>
{
    private Func<TResult>? suppiler;
    private volatile bool isCompleted = false;
    private TResult? result;
    private Queue<Action> queueWithContinueWithTasks;
    private MyThread[] arrayThreads;
    private Queue<Action> queueWithTasks;
    private Object locker = new Object();

    public MyTask(Func<TResult> suppiler, MyThread[] arrayThreads, Queue<Action> queueWithTasks)
    {
        this.suppiler = suppiler;
        queueWithContinueWithTasks = new ();
        this.arrayThreads = arrayThreads;
        this.queueWithTasks = queueWithTasks;
    }

    public TResult Result
    {
        get
        {
            while (!isCompleted) {}
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

    public Func<TResult> GetSupplier()
    {
        return suppiler;
    }

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

    public bool IsQueueEmpty()
    { 
        return queueWithContinueWithTasks.Count == 0;
    }

    public IMyTask<TNewResult> ContinueWith<TNewResult>(Func<TResult, TNewResult> suppiler)
    {
        var newTask = new MyTask<TNewResult>(() => suppiler(Result), arrayThreads, queueWithTasks);
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