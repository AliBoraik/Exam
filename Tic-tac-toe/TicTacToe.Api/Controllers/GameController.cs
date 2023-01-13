using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Domain.Enum;
using TicTacToe.Domain.Games;
using TicTacToe.Infrastructure;
using TicTacToe.Interfaces;

namespace TicTacToeGame.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{
    private readonly IGameRepository _repository;
    private readonly GameDataContext _ctx;

    public GameController(IGameRepository repository, GameDataContext ctx)
    {
        _repository = repository;
        _ctx = ctx;
    }
    
    [HttpGet]
    public async Task<IActionResult> All()
    {
        return Ok( await _repository.GetAllGames());
    }

    [HttpGet("CreateGame")]
    public async Task<IActionResult> CreateGame(string userId)
    {
        var player = await _repository.FindPlayer(userId);
        if (player == null)
            return Unauthorized();
        Game newGame = new Game();
        newGame.Status = GameStatus.InProgress;
        newGame.Players.Add(player);
        await _repository.CreateGame(newGame);
        return Ok();
    }
}