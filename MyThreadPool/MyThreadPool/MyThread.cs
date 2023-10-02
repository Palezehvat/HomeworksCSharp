using System.Reflection.Metadata.Ecma335;

namespace MyThreadPool;

public class MyThread<TResult>
{
    private volatile bool isActive = false;
    private volatile bool isAlive = true;
    private Thread? thread;
    private MyTask<TResult>? task;

    public MyThread()
    {
        thread = new Thread(() => EternalCycle());
        thread.Start();
    }

    public bool IsActive()
    {
        return isActive;
    }

    private void EternalCycle()
    {
        while (isAlive)
        {
            while (task == null || task.IsCompleted) { }
            isActive = true;
            try
            { 
                task.StartSuppiler();
            }
            catch (Exception ex) 
            {
                throw new AggregateException(ex);
            }

            isActive = false;
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

    public void GiveTask(MyTask<TResult> task)
    {
        if (isAlive)
        {
            this.task = task;
        }
    }
}
