using Microsoft.AspNetCore.Mvc;
using TicTacToe.Interfaces;

namespace TicTacToeGame.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{
    private readonly ILogger<GameController> _logger;
    private readonly IGameService _gameService;

    public GameController(ILogger<GameController> logger, IGameService gameService)
    {
        _logger = logger;
        _gameService = gameService;
    }
    
    [HttpGet]
    public IActionResult All()
    {
        return Ok(_gameService.GetAll());
    }
}