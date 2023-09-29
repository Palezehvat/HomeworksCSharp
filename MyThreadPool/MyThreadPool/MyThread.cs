namespace MyThreadPool;

public class MyThread<TResult>
{
    private volatile bool isActive = false;
    private Thread thread;
    public MyThread() {}

    public TResult AdddFunction(Func<TResult> resultFunction)
    {
        object value = null;
        while (isActive)
        {

        }
        thread = new Thread(() => { value = resultFunction(); });
        return (TResult)value;
    }

    public bool isActiveThread()
    {
        return isActive;
    }
}
