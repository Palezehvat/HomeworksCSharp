namespace MyThreadPool;

/// <summary>
/// An exception is thrown if, after calling the Shutdown method, the user calls other methods
/// </summary>
public class ShudownWasThrownException : Exception
{
    public ShudownWasThrownException() { }

    public ShudownWasThrownException(string? message) : base(message) { }

    public ShudownWasThrownException(string? message, Exception? innerException) : base(message, innerException) { }
}