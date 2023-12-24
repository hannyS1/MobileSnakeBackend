using MobileSnake.Api.Contracts.Data;

namespace MobileSnake.AppServices.Contracts.Services;

public interface IUserService
{
    public Task<UserDto> AuthenticateAsync(string name, string password);

    public Task<UserDto> Create(string name, string password);
    
    public Task<UserDto> GetByIdAsync(int id);
}