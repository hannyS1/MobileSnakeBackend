namespace MobileSnake.Common.Contracts.Exceptions;

public class MobileSnakeException : Exception
{
    public MobileSnakeException(string message) : base(message) { }

    public MobileSnakeException(string message, Exception exception) : base(message, exception) { }
}