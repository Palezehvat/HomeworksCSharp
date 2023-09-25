namespace Lazy;

/// <summary>
/// Class for checking Lazy
/// </summary>
public static class FunctionsForTests
{
    public static volatile int howMutchFunctionBeenCalled = 0;

    /// <summary>
    /// Function that counts how much it is caused by
    /// </summary>
    public static int FunctionForLazyWithCounter()
    {
        howMutchFunctionBeenCalled++;
        return howMutchFunctionBeenCalled;
    }

    /// <summary>
    /// Function that throws InvalidOperationException
    /// </summary>
    /// <returns></returns>
    public static int FunctionWithInvalidOperationException()
    {
        throw new InvalidOperationException();
    }
}