using System.IO;

namespace MyThreadPool;

public class MyThreadPool<TResult>
{
    private static MyThread<TResult>[] arrayThread;
    public MyThreadPool(int sizeThreads)
    {
        arrayThread = new MyThread<TResult>[sizeThreads];
        for (int i = 0; i < sizeThreads; i++)
        {
            arrayThread[i] = new MyThread<TResult>();
        }
    }

    public TResult GetTask(Func<TResult> suppiler)
    {
        TResult? result;
        while (true)
        {
            for (int i = 0; i < arrayThread.Length; i++)
            {
                if (!arrayThread[i].IsActive())
                {
                    var newTask = new MyTask<TResult>(suppiler);
                    arrayThread[i].GiveTask(newTask);
                    result = newTask.Result;
                    newTask = null;
                    return result;  
                }
            }
        }
    }

    public void Shutdown()
    {
        int disabledThreads = 0;
        while (disabledThreads < arrayThread.Length)
        {
            for (int i = 0; i < arrayThread.Length; ++i)
            {
                if (arrayThread[i].IsAlive() && !arrayThread[i].IsActive())
                {
                    arrayThread[i].KillThread();
                    ++disabledThreads;
                }
            }
        }
    }
}