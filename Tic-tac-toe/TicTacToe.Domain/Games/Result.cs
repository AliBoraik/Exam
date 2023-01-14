namespace TicTacToe.Domain.Games;

public class Result
{
    public Player? WinningPlayer { get; set; }
    public string GameId { get; set; }

    public Result(Player? winningPlayer, string gameId)
    {
        WinningPlayer = winningPlayer;
        GameId = gameId;
    }
}