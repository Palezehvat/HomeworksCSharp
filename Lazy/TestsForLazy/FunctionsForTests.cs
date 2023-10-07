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
    /// Function that throws InvalidOperationException
    /// </summary>
    /// <returns></returns>
    public int FunctionWithInvalidOperationException()
    {
        var firstNumber = 1;
        return firstNumber / 0;
    }
}