using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobileSnake.Api.Contracts.Data;
using MobileSnake.Api.Contracts.WebRoutes;
using MobileSnake.AppServices.Contracts.Services;
using MobileSnake.Host.Extensions;

namespace MobileSnake.Host.Controllers;

[Route(UserControllerWebRoutes.BasePath)]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IHttpContextAccessor _contextAccessor;
    
    public UserController(IUserService userService, IHttpContextAccessor contextAccessor)
    {
        _userService = userService;
        _contextAccessor = contextAccessor;
    }

    [Authorize]
    [HttpGet(UserControllerWebRoutes.GetCurrentUser)]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var user = await _userService.RetrieveFromHttpContext(_contextAccessor.HttpContext);
        if (user == null)
            return Unauthorized(new { message = "invalid token" });
        
        return Ok(user);
    }
}