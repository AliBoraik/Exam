using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TicTacToe.Infrastructure.Configurations;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgres(configuration);
        services.AddAuthentication(configuration);
        return services;
    }
}