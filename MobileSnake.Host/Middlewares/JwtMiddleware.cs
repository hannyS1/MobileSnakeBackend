using MobileSnake.AppServices.Contracts.Services;
using MobileSnake.Common.Contracts.Services;

namespace MobileSnake.Host.Middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    private readonly ITokenService _tokenService;
    
    public JwtMiddleware(RequestDelegate next, ITokenService tokenService)
    {
        _next = next;
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
    }

    public async Task Invoke(HttpContext context, IUserService userService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token != null)
            await AttachUserToContext(context, userService, token);
        await _next(context);
    }

    private async Task AttachUserToContext(HttpContext context, IUserService userService, string token)
    {
        var userId = _tokenService.GetUserIdByToken(token);
        var user = userId.HasValue ? await userService.GetByIdAsync(userId.Value) : null;
        context.Items["User"] = user;
    }

}