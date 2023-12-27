namespace Lazy;

/// <summary>
/// Class implements functions for checking Lazy
/// </summary>
public class FunctionsForTests
{
    private int howMuchFunctionBeenCalled = 0;

    /// <summary>
    /// Function that counts how much it is caused by
    /// </summary>
    public int FunctionForLazyWithCounter()
    {
        Interlocked.Increment(ref howMuchFunctionBeenCalled);
        return howMuchFunctionBeenCalled;
    }

    /// <summary>
    /// Function that throws DivideByZeroException
    /// </summary>
    /// <returns></returns>
    public int DivideByZeroException()
    {
        var firstNumber = 1;
        return firstNumber / 0;
    }
}