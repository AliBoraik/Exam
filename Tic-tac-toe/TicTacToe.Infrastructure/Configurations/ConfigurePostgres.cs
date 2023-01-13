using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TicTacToe.Infrastructure.Configurations;

public static class ConfigurePostgres
{
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        
        
        services.AddDbContext<GameDataContext>(options =>
        {
            var dockerEnv = Environment.GetEnvironmentVariable("CONNECTION_STRING_DOCKER");
           // options.UseNpgsql(dockerEnv ?? configuration.GetConnectionString("GameDB"));
           var connection = dockerEnv ?? configuration.GetConnectionString("GameDB");
           options.UseNpgsql(connection, b => b.MigrationsAssembly("TicTacToe.Api"));
        });
        
        return services;
    }
}