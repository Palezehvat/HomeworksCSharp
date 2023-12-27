namespace MyThreadPool;

/// <summary>
/// A class for automatic efficient flow control in the program.
/// </summary>
public class MyThreadPool
{
    private readonly MyThread[]? arrayThreads;
    private readonly Queue<Action> tasks = new();
    private readonly CancellationTokenSource token = new();
    private readonly Object lockerForThreads;
    private EventWaitHandle waitHandle;

    /// <summary>
    /// Constructor for creating n number of threads for tasks
    /// </summary>
    public MyThreadPool(int sizeThreads)
    {
        waitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
        lockerForThreads = new();
        if (sizeThreads <= 0)
        {
            throw new ArgumentOutOfRangeException();
        }
        arrayThreads = new MyThread[sizeThreads];
        for (int i = 0; i < sizeThreads; i++)
        {
            arrayThreads[i] = new(tasks, token.Token, lockerForThreads, waitHandle);
        }
    }

    /// <summary>
    /// Get number of threads
    /// </summary>
    public int GetNumberOfThreads()
    {
        if (arrayThreads == null)
        {
            throw new ArgumentNullException();
        }
        return arrayThreads.Length;
    }

    /// <summary>
    /// Accepts a function, adds it as a task in the thread, and returns the created task
    /// </summary>
    public IMyTask<TResult> Submit<TResult>(Func<TResult> supplier)
    {
        if (token.IsCancellationRequested)
        {
            throw new ShudownWasThrownException();
        }
        if (arrayThreads == null)
        {
            throw new ArgumentNullException();
        }
        var newTask = new MyTask<TResult>(supplier, token, this);
        tasks.Enqueue(() => newTask.StartSupplier());
        waitHandle.Set();

        return newTask;
    }

    /// <summary>
    /// Interrupts the processing of tasks that are not started do not begin, and those that are started are being completed
    /// </summary>
    public void Shutdown()
    {
        if (arrayThreads == null)
        {
            throw new ArgumentNullException(nameof(arrayThreads));
        }

        token.Cancel();
        waitHandle.Set();

        foreach(var thread in arrayThreads)
        {
            thread.Join();
        }
    }

    private class MyThread
    {
        private Thread? thread;
        private volatile Queue<Action> tasks;
        private Object locker;
        private CancellationToken token;
        private EventWaitHandle waitHandle;

        /// <summary>
        /// Task-based custom thread constructor
        /// </summary>
        public MyThread(Queue<Action> tasks, CancellationToken token, object locker, EventWaitHandle waitHandle)
        {
            this.waitHandle = waitHandle;
            this.tasks = tasks;
            this.token = token;
            this.locker = locker;
            thread = new Thread(EternalCycle);
            thread.Start();
        }

        /// <summary>
        /// Task waiting cycle
        /// </summary>
        private void EternalCycle()
        {
            Action? task = null;
            while (!token.IsCancellationRequested)
            {
                waitHandle.WaitOne();
                if (!token.IsCancellationRequested)
                {
                    lock (locker)
                    {
                        if (tasks.Count != 0)
                        {
                            var isCorrect = tasks.TryDequeue(out task);
                            if (!isCorrect)
                            {
                                throw new InvalidOperationException();
                            }
                        }
                        if (tasks.Count == 0)
                        {
                            waitHandle.Reset();
                        }
                    }
                    if (task != null)
                    {
                        task();
                    }
                    task = null;
                }
            }
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

    private class MyTask<TResult> : IMyTask<TResult>
    {
        private readonly Func<TResult>? supplier;
        private TResult? result;
        private Exception? exception;
        private CancellationTokenSource token;
        private ManualResetEvent resetEvent = new ManualResetEvent(false);
        private MyThreadPool? pool;

        /// <summary>
        /// Constructor for creating a task
        /// </summary>
        public MyTask(Func<TResult> supplier, CancellationTokenSource token, MyThreadPool pool)
        {
            this.supplier = supplier;
            this.token = token;
            this.pool = pool;
            this.result = default;
        }

        /// <summary>
        /// Get Result
        /// </summary>
        public TResult Result
        {
            get
            {
                if (waitHandle == null)
                {
                    throw new InvalidOperationException();
                }
                waitHandle.WaitOne();
                if (exception != null)
                {
                    throw new AggregateException(exception);
                }
                return result!;
            }
        }

        /// <summary>
        /// Check is completed Task and return result
        /// </summary>
        public bool IsCompleted { get; private set; }

        /// <summary>
        /// Task completion
        /// </summary>
        public void StartSupplier()
        {
            if (supplier != null)
            {
                try
                {
                    result = supplier();
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
                
                if (waitHandle == null)
                {
                    throw new InvalidOperationException();
                }

                waitHandle.Set();

                IsCompleted = true;
            }
        }

        /// <summary>
        /// A method for solving subtasks from the results obtained from the tasks
        /// </summary>
        public IMyTask<TNewResult> ContinueWith<TNewResult>(Func<TResult, TNewResult> supplier)
        {
            if (pool == null) 
            { 
                throw new InvalidOperationException(); 
            }
            
            if (!token.IsCancellationRequested)
            {
                return pool.Submit(() => supplier(Result));
            }
            throw new ShudownWasThrownException();
        }
    }
}