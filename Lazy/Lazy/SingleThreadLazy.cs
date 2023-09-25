namespace Lazy;

public class SingleThreadLazy<T> : ILazy<T>
{
    private Func<T> functionForLazy;
    private bool flagForCounting = false;
    private T resultSuppiler;
    private Exception exceptionFromSuppiler = default;

    /// <summary>
    /// Constructor for storing the object creation function
    /// </summary>
    public SingleThreadLazy(Func<T> function)
    {
        functionForLazy = function;
    }

    public T Get()
    {
        if (!flagForCounting)
        {
            flagForCounting = true;
            try
            {
                resultSuppiler = functionForLazy();
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