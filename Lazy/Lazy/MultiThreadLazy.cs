﻿namespace Lazy;

public class MultiThreadLazy<T> : ILazy<T>
{
    private Func<T> functionForLazy;
    private Object objectLock = new ();
    private volatile bool initialized = false;
    private T resultSuppiler;
    private Exception exceptionSuppiler = default;

    /// <summary>
    /// Constructor for storing the object creation function
    /// </summary>
    public MultiThreadLazy(Func<T> function)
    {
        functionForLazy = function;
    }

    public T Get()
    {
        if (initialized)
        {
            if (exceptionSuppiler != default)
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
                }
                if (exceptionSuppiler != default)
                {
                    throw exceptionSuppiler;
                }
                initialized = true;
            }
            return resultSuppiler;
        }
    }
}