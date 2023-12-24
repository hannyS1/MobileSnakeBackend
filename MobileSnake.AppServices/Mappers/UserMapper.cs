using MobileSnake.Api.Contracts.Data;
using MobileSnake.AppServices.Mappers.Interfaces;
using MobileSnake.Entities;

namespace MobileSnake.AppServices.Mappers;

internal class UserMapper : IUserMapper
{
    public UserDto Map(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
        };
    }
}