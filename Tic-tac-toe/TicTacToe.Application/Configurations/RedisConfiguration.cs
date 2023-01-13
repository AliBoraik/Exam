using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using TicTacToe.Interfaces;

namespace TicTacToe.Application.Configurations;

public static class RedisConfiguration
{
    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ICacheService,CacheService>();
        return services;
    }
}