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
    private Object locker = new Object();
    private CancellationToken token;

    /// <summary>
    /// Task-based custom thread constructor
    /// </summary>
    public MyThread(Queue<Action> tasks, CancellationToken token)
    {
        thread = new Thread(() => EternalCycle());
        thread.Start();
        this.tasks = tasks;
        this.token = token;
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
            while (tasks.Count == 0) { }
            if (token.IsCancellationRequested)
            {
                break;
            }
            lock (locker)
            {
                task = tasks.Dequeue();
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
}
