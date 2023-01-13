using Microsoft.AspNetCore.Mvc;
using TicTacToe.Domain.Games;
using TicTacToe.Interfaces;

namespace TicTacToeGame.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{
    private readonly IGameRepository _repository;

    public GameController(IGameRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet]
    public IActionResult All()
    {
        return Ok(_repository.GetAllGames());
    }

    [HttpGet("CreateGame")]
    public async Task<IActionResult> CreateGame(string userId)
    {
        var player = await _repository.FindPlayer(userId);
        if (player == null)
            throw new AggregateException("Player not auth!");
        Game newGame = new Game
        {
            Player1 = player
        };
        await _repository.CreateGame(newGame);
        return Ok();
    }
}