namespace MyThreadPool;

public class MyTask<TResult> : IMyTask<TResult>
{
    private Func<TResult>? suppiler;
    private volatile bool isCompleted = false;
    private TResult? result;

    public MyTask(Func<TResult> suppiler)
    {
        this.suppiler = suppiler;
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
        }
    }
}