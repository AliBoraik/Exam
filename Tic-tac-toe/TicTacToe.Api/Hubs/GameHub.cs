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
        // get player by Id
        var player = await _repository.FindPlayer(userId);
        if (player == null)
            throw new AggregateException("Player not auth!");
        // get Game by Id
        var game = await _repository.GetGame(groupName);
        Console.WriteLine(game.Board.Pieces.Length);
        if (game == null)
            throw new AggregateException($"Game with {groupName} not found!");
        // add ConnectionId to player
        player.ConnectionId = Context.ConnectionId;
        // add to group
        await Groups.AddToGroupAsync(player.ConnectionId, game.Id);
        // check if the player is first one or second 
        if (game.Players[0] == null)
        {
            // add first player
            player.PlayerSign = "X";
            game.Players.Add(player);
            game.IsFirstPlayersTurn = false;
        }
        else
        {
            // add second  player
            player.PlayerSign = "O";
            game.Players.Add(player);
            // send to players message (StartGame)
            await Clients.Group(game.Id).SendAsync("StartGame", game.Id);
        }
        await _repository.UpdateGame(game);
    }

    // TODO Рейтинг 
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