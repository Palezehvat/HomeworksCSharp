namespace Lazy;

public class SingleThreadLazy<T> : ILazy<T>
{
    private Func<T> functionForLazy;
    private bool flagForCounting = false;
    private T resultSuppiler;
    private Exception exceptionFromSuppiler = null;
    public SingleThreadLazy(Func<T> function)
    {
        functionForLazy = function;
    }

    public T Get()
    {
        if (!flagForCounting)
        {
            try
            {
                resultSuppiler = functionForLazy();
            }
            catch (Exception exception)
            {
                exceptionFromSuppiler = exception;
                throw exceptionFromSuppiler;
            }
            flagForCounting = true;
            return resultSuppiler;
        }
        else
        {
            if (exceptionFromSuppiler != null)
            {
                throw exceptionFromSuppiler;
            }
            return resultSuppiler;
        }
    }
}