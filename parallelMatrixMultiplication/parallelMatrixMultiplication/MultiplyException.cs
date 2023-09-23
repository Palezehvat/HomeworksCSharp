namespace parallelMatrixMultiplication;

/// <summary>
/// An exception is thrown when the results of parallel and sequential multiplication do not match
/// </summary>
public class MultiplyException : Exception
{
    public MultiplyException() {}

    public MultiplyException(string? message) : base(message) {}

    public MultiplyException(string? message, Exception? innerException) : base(message, innerException) {}
}