using MobileSnake.AppServices.Contracts.Services;
using MobileSnake.AppServices.Mappers;
using MobileSnake.AppServices.Mappers.Interfaces;
using MobileSnake.AppServices.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MobileSnake.AppServices.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddSingleton<IUserMapper, UserMapper>();
        services.AddSingleton<IResultMapper, ResultMapper>();
        
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IResultService, ResultService>();
        return services;
    }
}