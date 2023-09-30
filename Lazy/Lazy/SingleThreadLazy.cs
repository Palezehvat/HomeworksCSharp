namespace Lazy;

public class SingleThreadLazy<T> : ILazy<T>
{
    private Func<T> supplier;
    private bool isCalculated = false;
    private T? resultSuppiler;
    private Exception? exceptionFromSuppiler = default;

    /// <summary>
    /// Constructor for storing the object creation function
    /// </summary>
    public SingleThreadLazy(Func<T> function)
    {
        supplier = function;
    }

    public T? Get()
    {
        if (!isCalculated)
        {
            isCalculated = true;
            try
            {
                resultSuppiler = supplier();
                supplier = null;
            }
            catch (Exception exception)
            {
                exceptionFromSuppiler = exception;
            }
            if (exceptionFromSuppiler != default)
            {
                throw exceptionFromSuppiler;
            }
            return resultSuppiler;
        }
        else
        {
            if (exceptionFromSuppiler != default)
            {
                throw exceptionFromSuppiler;
            }
            return resultSuppiler;
        }
    }
}