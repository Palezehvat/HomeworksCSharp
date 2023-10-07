namespace Lazy;

public class SingleThreadLazy<T> : ILazy<T>
{
    private Func<T>? supplier;
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
                exceptionFromSuppiler = ex;
            }
            finally
            {
                supplier = null;
                GC.Collect();
            }
            isCalculated = true;
        }
        if (exceptionFromSuppiler != default)
        {
            throw exceptionFromSuppiler;
        }
        return resultSuppiler;
    }
}