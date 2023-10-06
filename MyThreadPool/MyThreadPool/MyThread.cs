namespace MyThreadPool;

public class MyThread
{
    private volatile bool isActive = false;
    private volatile bool isAlive = true;
    private Thread? thread;
    private volatile Queue<Action> tasks;
    private Object locker = new Object();

    public MyThread(Queue<Action> tasks)
    {
        thread = new Thread(() => EternalCycle());
        thread.Start();
        this.tasks = tasks;
    }

    public bool IsActive()
    {
        return isActive;
    }

    private void EternalCycle()
    {
        Action? task = null;
        while (isAlive)
        {
            while (tasks.Count == 0) { }
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

    public void KillThread()
    {
        isAlive = false;
    }

    public bool IsAlive()
    {
        return isAlive;
    }
}
