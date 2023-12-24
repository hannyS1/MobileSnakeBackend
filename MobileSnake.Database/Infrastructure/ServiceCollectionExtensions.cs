using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MobileSnake.Database;

namespace MobileSnake.Database.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEntitiesWithEfSqlite(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }
        
        var connectionString = configuration.GetConnectionString("SqliteConnection");
        services.AddDbContext<ApplicationContext>(options => options.UseSqlite(
            connectionString, b => b.MigrationsAssembly("MobileSnake.Host")));
        
        return services;
    }
}