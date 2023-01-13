using Microsoft.AspNetCore.SignalR;
using TicTacToe.Domain.Enum;
using TicTacToe.Domain.Games;
using TicTacToe.Interfaces;

namespace TicTacToeGame.Api.Hubs;

public class GameHub : Hub
{
    private readonly IGameRepository _repository;

    public GameHub(IGameRepository repository)
    {
        _repository = repository;
    }
    public async Task JoinToGame(string groupName,string userId)
    {
        var player = await _repository.FindPlayer(userId);
        if (player == null)
            throw new AggregateException("Player not auth!");
        
        var game = await _repository.GetGame(groupName);
        if (game == null)
            throw new AggregateException($"Game with {groupName} not found!");
        
        // player1 already waiting...  
        game.Player2 = player;
        game.Player1.PlayerSign = "X";
        game.Player2.PlayerSign = "O";
        game.IsFirstPlayersTurn = false;
        await Groups.AddToGroupAsync(game.Player1.ConnectionId, game.Id);
        await Groups.AddToGroupAsync(game.Player2.ConnectionId, game.Id);
        await Clients.Group(game.Id).SendAsync("GroupName", game.Id);
        await _repository.UpdateGame(game);
    }

    public async Task PlacePiece(string groupName, int index)
    {
        Game? game = await _repository.GetGame(groupName);
        if (game == null)
            throw new AggregateException($"Game with {groupName} not found!");
        
        UpdateBoard? updateBoard = game.PlacePieceToBoard(index, game.GetPlayerById(Context.ConnectionId).PlayerSign);
        
        if (updateBoard == null) return;
        
        await Clients.Group(groupName).SendAsync("UpdateBoard", updateBoard.Pieces);
        if (game.IsGameOver())
        {
            game.Status = GameStatus.completed;
            var result = game.Result.WinningPlayer == null ? "Game DRAW" : $"{game.Result.WinningPlayer.Name} WIN";
            await Clients.Group(groupName).SendAsync("GameOver", result);
        }
    }
    
    public async Task RestartGame(string groupName)
    {
        var game = await _repository.GetGame(groupName);
        game.RestartGame();
        await _repository.UpdateGame(game);
        await Clients.Group(groupName).SendAsync("UpdateBoard", game.Board.Pieces);
    }
}