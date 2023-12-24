using Microsoft.EntityFrameworkCore;
using MobileSnake.Api.Contracts.Data;
using MobileSnake.AppServices.Contracts.Services;
using MobileSnake.AppServices.Mappers.Interfaces;
using MobileSnake.Common.Contracts.Exceptions;
using MobileSnake.Database;
using MobileSnake.Entities;

namespace MobileSnake.AppServices.Services;

internal class UserService : IUserService
{
    private readonly ApplicationContext _dbContext;
    private readonly IUserMapper _userMapper;

    public UserService(ApplicationContext dbContext, IUserMapper userMapper)
    {
        _userMapper = userMapper ?? throw new ArgumentNullException(nameof(userMapper));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    
    public async Task<UserDto> AuthenticateAsync(string name, string password)
    {
        var user = await _dbContext.Users.Where(u => u.Name == name && u.Password == password).FirstOrDefaultAsync();
        if (user == null)
            throw new MobileSnakeException("invalid login or password");
        return _userMapper.Map(user);
    }

    public async Task<UserDto> Create(string name, string password)
    {
        var isUserExist = await _dbContext.Users.Where(u => u.Name == name).FirstOrDefaultAsync() != null;
        if (isUserExist)
            throw new MobileSnakeException("user already exist");
        
        var userEntry = await _dbContext.Users.AddAsync(new User { Name = name, Password = password });
        await _dbContext.SaveChangesAsync();
        return _userMapper.Map(userEntry.Entity);
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        // var user = await _dbContext.Users.FindAsync(id);
        // var user2 = await _dbContext.Users.Join(_dbContext.Results,
        //     u => u.Id,
        //     r => r.UserId, (u, r) => new UserDto
        //     {
        //         Id = u.Id,
        //         Name = u.Name,
        //         Record = r.Score
        //     }).ToListAsync();
        var user = await _dbContext.Users.FindAsync(id);
        if (user == null)
            throw new MobileSnakeException($"user with id {id} not found");
        
        var bestUserScore = await _dbContext.Results.Where(r => r.UserId == user.Id).MaxAsync(r => r.Score);
        
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Record = bestUserScore
        };
    }
}