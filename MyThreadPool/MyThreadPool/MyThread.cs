namespace MyThreadPool;

/// <summary>
/// A class of native threads responsible for executing tasks
/// </summary>
public class MyThread
{
    private volatile bool isActive = false;
    private volatile bool isAlive = true;
    private Thread? thread;
    private volatile Queue<Action> tasks;
    private Object locker;
    private CancellationToken token;

    /// <summary>
    /// Task-based custom thread constructor
    /// </summary>
    public MyThread(Queue<Action> tasks, CancellationToken token, object locker)
    {
        this.tasks = tasks;
        this.token = token;
        this.locker = locker;
        thread = new Thread(() => EternalCycle());
        thread.Start();
    }

    /// <summary>
    /// Checks if the thread is busy
    /// </summary>
    public bool IsActive()
    {
        return isActive;
    }

    /// <summary>
    /// Task waiting cycle
    /// </summary>
    private void EternalCycle()
    {
        Action? task = null;
        while (isAlive)
        {
            while (tasks.Count == 0 && isAlive) { }
            if (token.IsCancellationRequested)
            {
                break;
            }
            if (isAlive)
            {
                lock (locker)
                {
                     if (tasks.Count != 0)
                     {
                        task = tasks.Dequeue();
                     }
                     isActive = true;
                }
                if (task != null)
                {
                    isActive = true;
                    try
                    { 
                        task();
                    }
                    catch (Exception ex) 
                    {
                        throw new AggregateException(ex);
                    }
                    isActive = false;
                }
                isActive = false;
                task = null;
            }
        }
    }

    /// <summary>
    /// Eliminating threads
    /// </summary>
    public void KillThread()
    {
        isAlive = false;
    }

    /// <summary>
    /// Checking whether the thread is alive
    /// </summary>
    public bool IsAlive()
    {
        return isAlive;
    }

    /// <summary>
    /// Suspends the current thread
    /// </summary>
    public void Join()
    {
        while (thread.IsAlive) { }
        thread.Join();
    }
}
