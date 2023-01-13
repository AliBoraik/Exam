using Microsoft.AspNetCore.SignalR;
using TicTacToe.Domain.Games;
using TicTacToe.Interfaces;

namespace TicTacToeGame.Api.Hubs;

public class GameHub : Hub
{
    private readonly IGameService _gameService;

    public GameHub(IGameService gameService)
    {
        _gameService = gameService;
    }

    public async Task FindGame(string username)
    {
        Player? newPlayer = new Player
        {
            Name = username,
            ConnectionId = Context.ConnectionId
        };
        var game = _gameService.FindFreeGame();
        if (game != null)
        {
            // player one already waiting...  
            game.Player2 = newPlayer;
            game.Player1.PlayerSign = "X";
            game.Player2.PlayerSign = "O";
            game.IsFirstPlayersTurn = false;
            await Groups.AddToGroupAsync(game.Player1.ConnectionId, game.Id);
            await Groups.AddToGroupAsync(game.Player2.ConnectionId, game.Id);
            await Clients.Group(game.Id).SendAsync("GroupName", game.Id);
        }
        else
        {
            Game newGame = new Game
            {
                Player1 = newPlayer
            };
            _gameService.Create(newGame);
        }
    }

    public async Task PlacePiece(string groupName, int index)
    {
        Game game = _gameService.Get(groupName);
        UpdateBoard? updateBoard = game.PlacePieceToBoard(index, game.GetPlayerById(Context.ConnectionId).PlayerSign);
        
        if (updateBoard == null) return;
        
        await Clients.Group(groupName).SendAsync("UpdateBoard", updateBoard.Pieces);
        if (game.IsGameOver())
        {
            var result = game.Result.WinningPlayer == null ? "Game DRAW" : $"{game.Result.WinningPlayer.Name} WIN";
            await Clients.Group(groupName).SendAsync("GameOver", result);
        }
    }
    
    public async Task RestartGame(string groupName)
    {
        var game = _gameService.Get(groupName);
        game.RestartGame();
        await Clients.Group(groupName).SendAsync("UpdateBoard", game.Board.Pieces);
    }
    public async Task JoinToGame(string groupName,string username)
    {
        Player newPlayer = new Player
        {
            Name = username,
            ConnectionId = Context.ConnectionId
        };
        var game = _gameService.Get(groupName);
        // player one already waiting...  
        game.Player2 = newPlayer;
        game.Player1.PlayerSign = "X";
        game.Player2.PlayerSign = "O";
        game.IsFirstPlayersTurn = false;
        await Groups.AddToGroupAsync(game.Player1.ConnectionId, game.Id);
        await Groups.AddToGroupAsync(game.Player2.ConnectionId, game.Id);
        await Clients.Group(game.Id).SendAsync("GroupName", game.Id);
    }

    // public override Task OnDisconnectedAsync(Exception? exception)
    // {
    //     _gameService.Remove(Context.ConnectionId);
    //     return base.OnDisconnectedAsync(exception);
    // }
}