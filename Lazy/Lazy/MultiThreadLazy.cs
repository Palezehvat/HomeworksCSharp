namespace Lazy;

public class MultiThreadLazy<T> : ILazy<T>
{
    private Func<T>? supplier;
    private readonly Object objectLock = new ();
    private volatile bool isCalculated = false;
    private T? resultSuppiler;
    private Exception? exceptionSuppiler = default;

    /// <summary>
    /// Constructor for storing the object creation function
    /// </summary>
    public MultiThreadLazy(Func<T> function)
    {
        supplier = function;
    }

    public T? Get()
    {
        if (!isCalculated)
        {
            lock (objectLock)
            {
                if (!isCalculated)
                {
                    try
                    {
                        if (supplier == null)
                        {
                            resultSuppiler = default;
                        }
                        else
                        {
                            resultSuppiler = supplier();
                        }
                    }
                    catch (Exception ex)
                    {
                        exceptionSuppiler = ex;
                    }
                    finally
                    {
                        supplier = null;
                        GC.Collect();
                    }
                    isCalculated = true;
                }
            }
        }

        if (exceptionSuppiler != default)
        {
            throw exceptionSuppiler;
        }
        return resultSuppiler;
    }
}