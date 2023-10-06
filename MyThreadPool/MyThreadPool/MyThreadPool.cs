﻿namespace MyThreadPool;

/// <summary>
/// A class for automatic efficient flow control in the program.
/// </summary>
public class MyThreadPool
{
    private static MyThread[]? arrayThreads;
    private static Queue<Action> tasks = new ();
    private CancellationToken token = new CancellationToken();

    /// <summary>
    /// Constructor for creating n number of threads for tasks
    /// </summary>
    public MyThreadPool(int sizeThreads)
    {
        if (sizeThreads <= 0)
        {
            throw new ArgumentOutOfRangeException();
        }
        arrayThreads = new MyThread[sizeThreads];
        for (int i = 0; i < sizeThreads; i++)
        {
            arrayThreads[i] = new MyThread(tasks, token);
        }
    }

    /// <summary>
    /// Accepts a function, adds it as a task in the thread, and returns the created task
    /// </summary>
    public IMyTask<TResult> Submit<TResult>(Func<TResult> suppiler)
    {
        var newTask = new MyTask<TResult>(suppiler, arrayThreads, tasks);
        tasks.Enqueue(() => newTask.StartSuppiler());
        return newTask;
    }

    /// <summary>
    /// Interrupts the processing of tasks that are not started do not begin, and those that are started are being completed
    /// </summary>
    public void Shutdown(CancellationToken tokenFromUser)
    {
        if (tokenFromUser.IsCancellationRequested)
        {
            token.ThrowIfCancellationRequested();
        }
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