using MobileSnake.Api.Contracts.Data;
using MobileSnake.Entities;

namespace MobileSnake.AppServices.Mappers.Interfaces;

public interface IUserMapper
{
    public UserDto Map(User user);
}