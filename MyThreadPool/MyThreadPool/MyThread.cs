using System.Diagnostics;

namespace MyThreadPool;

/// <summary>
/// A class of native threads responsible for executing tasks
/// </summary>
public class MyThread
{
    private volatile bool isActive = false;
    private Thread? thread;
    private volatile Queue<Action> tasks;
    private volatile bool isAlive = true;
    private Object locker;
    private CancellationTokenSource token;

    /// <summary>
    /// Task-based custom thread constructor
    /// </summary>
    public MyThread(Queue<Action> tasks, CancellationTokenSource token, object locker)
    {
        this.tasks = tasks;
        this.token = token;
        this.locker = locker;
        thread = new Thread(EternalCycle);
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
        while (!token.IsCancellationRequested)
        {
            while (!(tasks.Count == 0))
            {
                if (token.IsCancellationRequested)
                {
                    lock (locker)
                    {
                        isAlive = false;
                        return;
                    }
                }
                lock (locker)
                {
                     if (tasks.Count != 0)
                     {
                        tasks.TryDequeue(out task);
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
        lock (locker)
        {
            isAlive = false;
            return;
        }
    }

    public bool GetIsAlive()
    {
        return isAlive;
    }

    /// <summary>
    /// Suspends the current thread
    /// </summary>
    public void Join()
    {
        if (thread == null)
        {
            throw new ArgumentNullException(nameof(thread));
        }
        
        thread.Join();
    }
}
