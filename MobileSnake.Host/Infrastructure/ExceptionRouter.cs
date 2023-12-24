using MobileSnake.Common.Contracts.Exceptions;

namespace MobileSnake.Host.Infrastructure;

public static class ExceptionRouter
{
    public static ApiResult Route(Exception exception)
    {
        if (exception is MobileSnakeException)
        {
            return new ApiResult { StatusCode = StatusCodes.Status400BadRequest, ContentType = "text" };
        }

        throw exception;
    }
}