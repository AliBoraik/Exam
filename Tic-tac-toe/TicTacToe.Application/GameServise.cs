using TicTacToe.Domain.Games;
using TicTacToe.Interfaces;

namespace TicTacToe.Application;

public class GameService : IGameService
{
    private static readonly List<Game?> _games = new();

    public List<Game?> GetAll()
    {
        return _games;
    }

    public Game Get(string id)
    {
        return _games.FirstOrDefault(game => game?.Id == id)!;
    }

    public void Create(Game game)
    {
        if (_games.Any(game1 => game1?.Id == game.Id))
        {
            return;
        }
        _games.Add(game);
    }

    public void Remove(string id)
    {
        foreach (Game? game in _games.ToList())
        {
            if (game == null) continue;
            if (game.Player1.ConnectionId == id || game.Player2.ConnectionId == id)
            {
                _games.Remove(game);
            } 
        }
    }

    public Game? FindFreeGame()
    {
        return _games.FirstOrDefault(game => game is { IsFirstPlayersTurn: true });
    }

    public int Get()
    {
        return _games.Count;
    }
}