using System.Reflection.Metadata.Ecma335;

namespace Lazy;

public class MultiThreadLazy<T> : ILazy<T>
{
    private Func<T> functionForLazy;
    private Object objectLock = new ();
    private volatile bool initialized = false;
    private T resultSuppiler;
    private Exception exceptionSuppiler = null;

    public MultiThreadLazy(Func<T> function)
    {
        functionForLazy = function;
    }

    public T Get()
    {
        if (initialized)
        {
            if (exceptionSuppiler != null)
            {
                throw exceptionSuppiler;
            }
            return resultSuppiler;
        }

        lock (objectLock)
        {
            if (!initialized)
            {
                try
                {
                    resultSuppiler = functionForLazy();
                }
                catch (Exception ex)
                {
                    exceptionSuppiler = ex;
                    throw;
                }
                initialized = true;
            }
            return resultSuppiler;
        }
    }
}