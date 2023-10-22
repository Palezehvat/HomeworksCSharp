namespace SimpleFTP;

internal class CommandIncorrectException : Exception
{
    public CommandIncorrectException() {}

    public CommandIncorrectException(string? message) : base(message) {}

    public CommandIncorrectException(string? message, Exception? innerException) : base(message, innerException) {}
}