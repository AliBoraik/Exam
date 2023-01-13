using Microsoft.AspNetCore.Mvc;
using TicTacToe.Interfaces;

namespace TicTacToeGame.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CacheController : ControllerBase
{
    private readonly ILogger<CacheController> _logger;
    private readonly ICacheService _cache;

    public CacheController(ILogger<CacheController> logger, ICacheService cache)
    {
        _logger = logger;
        _cache = cache;
    }
    
    [HttpPost("cache")]
    public async Task<IActionResult> Set(string key, string value)
    {
        var result = await _cache.SetData(key, value);
        return Ok(result);
    }
    
    [HttpGet("cache")]
    public async Task<IActionResult> Get(string key)
    {
        var result = await _cache.GetData(key);
        return Ok(result);
    }
}