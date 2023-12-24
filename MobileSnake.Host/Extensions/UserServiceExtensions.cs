using MobileSnake.Api.Contracts.Data;
using MobileSnake.AppServices.Contracts.Services;

namespace MobileSnake.Host.Extensions;

public static class UserServiceExtensions
{
    public static async Task<UserDto> RetrieveFromHttpContext(this IUserService service, HttpContext context)
    {
        if (context == null)
            return null;
        
        return await service.GetByIdAsync(context.GetUserId());
    }
}