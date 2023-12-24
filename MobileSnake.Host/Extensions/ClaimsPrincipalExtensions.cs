using System.Security.Claims;

namespace MobileSnake.Host.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static int RetrieveId(this ClaimsPrincipal user)
    {
        var userIdString = user.FindFirstValue("id");
        return int.Parse(userIdString);
    }
}