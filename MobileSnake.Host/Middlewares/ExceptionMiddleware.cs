using MobileSnake.Common.Contracts.Exceptions;
using MobileSnake.Host.Infrastructure;

namespace MobileSnake.Host.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger,
        IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (MobileSnakeException e)
        {
            var result = ExceptionRouter.Route(e);
            context.Response.StatusCode = result.StatusCode;
            context.Response.ContentType = result.ContentType;
            await context.Response.WriteAsync(e.Message);
        }
    }
}