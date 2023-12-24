
using Microsoft.OpenApi.Models;
using MobileSnake.AppServices.Infrastructure;
using MobileSnake.Common.Infrastructure;
using MobileSnake.Database.Infrastructure;
using MobileSnake.Host.Middlewares;

namespace MobileSnake.Host.Infrastructure;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddHttpContextAccessor();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Mobile snake API", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });
        });
        
        services.AddAppServices();
        services.AddEntitiesWithEfSqlite(_configuration);
        services.AddJwtAuthentication(_configuration);
        services.AddPasswordHasher();
        
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseMiddleware<JwtMiddleware>();
        
        app.UseHttpsRedirection();
        
        app.UseRouting();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseCors(builder => builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithExposedHeaders("*"));
        
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
        });
    }
}