namespace MyThreadPool;

public class MyThreadPool<TResult>
{
    private static MyThread<TResult>[] arrayThread;
    public MyThreadPool(int sizeThreads)
    {
        arrayThread = new MyThread<TResult>[sizeThreads];
        for (int i = 0; i < sizeThreads; i++)
        {

        }
    }

    public void Shutdown()
    {

    }
}