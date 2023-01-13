using System.Dynamic;
using TicTacToe.Domain.Games;

namespace TicTacToe.Interfaces;


public interface IGameService
{
    List<Game?> GetAll();

    Game Get(string id);
    
    void Create(Game game);

    void Remove(string id);

    Game? FindFreeGame();

    int Get();
}