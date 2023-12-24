using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobileSnake.Api.Contracts.Data;
using MobileSnake.Api.Contracts.WebRoutes;
using MobileSnake.AppServices.Contracts.Services;
using MobileSnake.Host.Extensions;

namespace MobileSnake.Host.Controllers;

[Route(ResultControllerWebRoutes.BasePath)]
public class ResultController : Controller
{
    private readonly IResultService _resultService;
    private readonly IUserService _userService;
    private readonly IHttpContextAccessor _contextAccessor;
    
    public ResultController(IResultService resultService, IUserService userService, IHttpContextAccessor contextAccessor)
    {
        _resultService = resultService ?? throw new ArgumentNullException(nameof(resultService));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
    }
    
    [Authorize]
    [HttpGet(ResultControllerWebRoutes.GetCurrentUserResults)]
    public async Task<ActionResult<List<ResultDto>>> GetCurrentUserResults()
    {
        var user = await _userService.RetrieveFromHttpContext(_contextAccessor.HttpContext);
        if (user == null)
            return Unauthorized(new { message = "invalid token" });

        return await _resultService.GetByUserId(user.Id);
    }

    [HttpGet(ResultControllerWebRoutes.GetRecords)]
    public async Task<ActionResult<List<ResultDto>>> GetRecords()
    {
        return await _resultService.GetRecords();
    }

    [Authorize]
    [HttpPost(ResultControllerWebRoutes.CreateResult)]
    public async Task<ActionResult<ResultDto>> Create([FromBody] CreateResultDto dto)
    {
        var user = await _userService.RetrieveFromHttpContext(_contextAccessor.HttpContext);
        if (user == null)
            return Unauthorized(new { message = "invalid token" });

        return await _resultService.Create(dto, user.Id);
    }
}