using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MobileSnake.Common.Contracts.Options;
using MobileSnake.Common.Contracts.Services;
using MobileSnake.Common.Services;

namespace MobileSnake.Common.Infrastructure;

public static class ServiceCollectionExtensions
{
    public const string JwtOptionsPath = "Jwt";
    
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (configuration == null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }
        
        var jwtOptions = configuration.GetSection(JwtOptionsPath).Get<JwtOptions>();

        services.Configure<JwtOptions>(configuration.GetSection(JwtOptionsPath));
        
        services.AddSingleton<ITokenService, JwtTokenService>();
            
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;    
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
            };
        });
        
        return services;
    }

    public static IServiceCollection AddPasswordHasher(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        return services;
    }
}