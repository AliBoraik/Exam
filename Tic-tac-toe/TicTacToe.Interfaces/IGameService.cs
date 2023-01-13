using System.Dynamic;
using Microsoft.AspNetCore.Identity;
using TicTacToe.Domain.Games;

namespace TicTacToe.Interfaces;


public interface IGameRepository
{
    Task<List<Game>> GetAllGames();

    Task<Game?> GetGame(string id);
    
    Task<string> CreateGame(Game? game);
    Task<bool> UpdateGame(Game? game);

    Task<Player?> FindPlayer(string id);

    Task<Game?> FindFreeGame();

    Task CreatePlayer(Player player);


}