using System.Net.Http.Headers;

namespace Lazy;

public static class FunctionsForTests
{
    public static volatile int howMutchFunctionBeenCalled = 0;
    public static int FunctionForLazyWithCounter()
    {
        howMutchFunctionBeenCalled++;
        return howMutchFunctionBeenCalled;
    }

    public static int FunctionWithInvalidOperationException()
    {
        throw new InvalidOperationException();
    }
}