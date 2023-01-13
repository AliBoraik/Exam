using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicTacToe.Domain.Enum;

namespace TicTacToe.Infrastructure.Configurations;

public static class ConfigurePostgres
{
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        // For Entity Framework  
        services.AddDbContext<GameDataContext>(options =>
        {
            var dockerEnv = Environment.GetEnvironmentVariable("CONNECTION_STRING_DOCKER");
           // options.UseNpgsql(dockerEnv ?? configuration.GetConnectionString("GameDB"));
           var connection = dockerEnv ?? configuration.GetConnectionString("GameDB");
           options.UseNpgsql(connection, b => b.MigrationsAssembly("TicTacToe.Api"));
        });
        // For Identity  
        services.AddIdentity<IdentityUser, IdentityRole>(options=> {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            }) 
            .AddEntityFrameworkStores<GameDataContext>()  
            .AddDefaultTokenProviders();  
      
        return services;
    }
}