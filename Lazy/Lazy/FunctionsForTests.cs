namespace Lazy;

/// <summary>
/// Class for checking Lazy
/// </summary>
public class FunctionsForTests
{
    public volatile int howMuchFunctionBeenCalled = 0;

    /// <summary>
    /// Function that counts how much it is caused by
    /// </summary>
    public int FunctionForLazyWithCounter()
    {
        howMuchFunctionBeenCalled++;
        return howMuchFunctionBeenCalled;
    }

    /// <summary>
    /// Function that throws InvalidOperationException
    /// </summary>
    /// <returns></returns>
    public int FunctionWithInvalidOperationException()
    {
        throw new InvalidOperationException();
    }
}