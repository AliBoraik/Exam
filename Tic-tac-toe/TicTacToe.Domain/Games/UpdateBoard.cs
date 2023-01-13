namespace TicTacToe.Domain.Games;

public class UpdateBoard
{
    public string[] Pieces { get; }

    public UpdateBoard(string[] pieces)
    {
        Pieces = pieces;
    }
}