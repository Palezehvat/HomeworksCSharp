namespace MyThreadPool;

public class MyThreadPool
{
    private static MyThread[]? arrayThreads;
    private static Queue<Action> tasks = new ();
    public MyThreadPool(int sizeThreads)
    {
        arrayThreads = new MyThread[sizeThreads];
        for (int i = 0; i < sizeThreads; i++)
        {
            arrayThreads[i] = new MyThread(tasks);
        }
    }

    public IMyTask<TResult> Submit<TResult>(Func<TResult> suppiler)
    {
        var newTask = new MyTask<TResult>(suppiler, arrayThreads, tasks);
        tasks.Enqueue(() => newTask.StartSuppiler());
        return newTask;
    }

    public void Shutdown()
    {
        int disabledThreads = 0;
        while (disabledThreads < arrayThreads.Length)
        {
            for (int i = 0; i < arrayThreads.Length; ++i)
            {
                if (arrayThreads[i].IsAlive() && !arrayThreads[i].IsActive())
                {
                    arrayThreads[i].KillThread();
                    ++disabledThreads;
                }
            }
        }
    }
}