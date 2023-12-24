namespace MobileSnake.Host.Extensions;

public static class HttpContextExtensions
{
    public static int GetUserId(this HttpContext context)
    {
        return context.User.RetrieveId();
    }
}