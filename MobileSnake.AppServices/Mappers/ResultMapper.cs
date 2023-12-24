using MobileSnake.Api.Contracts.Data;
using MobileSnake.AppServices.Mappers.Interfaces;
using MobileSnake.Entities;

namespace MobileSnake.AppServices.Mappers;

internal class ResultMapper : IResultMapper
{
    private readonly IUserMapper _userMapper;
    
    public ResultMapper(IUserMapper userMapper)
    {
        _userMapper = userMapper ?? throw new ArgumentNullException(nameof(userMapper));
    }
    
    public ResultDto Map(Result result)
    {
        return new ResultDto
        {
            Id = result.Id,
            CreationDateTime = result.CreationDateTime,
            Score = result.Score,
            User = _userMapper.Map(result.User),
        };
    }
}