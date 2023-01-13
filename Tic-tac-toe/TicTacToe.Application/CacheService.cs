using System.Reflection.Metadata.Ecma335;
using StackExchange.Redis;
using TicTacToe.Interfaces;

namespace TicTacToe.Application;

public class CacheService : ICacheService
{
    private readonly IDatabase _db;

    public CacheService()
    {
        var multiplexer = ConnectionMultiplexer.ConnectAsync("localhost:6379");
        _db = multiplexer.Result.GetDatabase();
    }

    public async Task<bool> SetData(string key, string value)
    {
         return await _db.StringSetAsync(key, value);
    }

    public async Task<string?> GetData(string key)
    {
       return await _db.StringGetAsync(key).ConfigureAwait(false);
    }
}