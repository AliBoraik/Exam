using TicTacToe.Domain;
using Microsoft.AspNetCore.Mvc;

namespace TicTacToeGame.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private static readonly List<string> users = new()
    {
        "Ali"
    };
    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger)
    {
        _logger = logger;
    }
    
    [HttpPost("login")]
    public IActionResult LogIn(User user)
    {
        if (users.Any(u => user.Name.Equals(u)))
        {
            return BadRequest();
        } 
        //users.Add(user.Name);
       return Ok();
    }
    
    [HttpPost("all")]
    public IActionResult All()
    {
        return Ok(users);
    }
}