using System.ComponentModel.DataAnnotations;
using static System.Guid;

namespace TicTacToe.Domain.Games
{
    public class Game
    {
        [Key]
        public string Id { get; set; }
        public Player Player1 { get; init; } = null!;
        public Player Player2 { get; set; } = null!;
        public Board Board;
        public bool IsFirstPlayersTurn;
        private readonly int[,] _winCombination;
        public Result? Result;
        private bool IsFirstPlayerPlaying;

        public Game()
        {
            Id = NewGuid().ToString("d");
            IsFirstPlayersTurn = true;
            Board = new Board();
            _winCombination = new[,]
            {
                { 0, 1, 2 },
                { 3, 4, 5 },
                { 6, 7, 8 },
                { 0, 3, 6 },
                { 1, 4, 7 },
                { 2, 5, 8 },
                { 0, 4, 8 },
                { 2, 4, 6 }
            };
        }

        public bool IsGameOver()
        {
            for (int i = 0; i < 8; i++)
            {
                //* X win check
                if (Board.Pieces[_winCombination[i, 0]] == Player1.PlayerSign
                    && Board.Pieces[_winCombination[i, 1]] == Player1.PlayerSign
                    && Board.Pieces[_winCombination[i, 2]] == Player1.PlayerSign)
                {
                    Result = new Result(Player1, Id);
                    //Result = $"{Player1.Name} WON";
                    return true;
                }
                //* O win check
                if (Board.Pieces[_winCombination[i, 0]] == Player2.PlayerSign
                    && Board.Pieces[_winCombination[i, 1]] == Player2.PlayerSign
                    && Board.Pieces[_winCombination[i, 2]] == Player2.PlayerSign)
                {
                    Result = new Result(Player2, Id);
                    //Result = $"{Player2.Name} WON";
                    return true;
                }
            }
            // Draw game check
            if (Board.Pieces.Any(boardPiece => boardPiece.Equals(""))) return false;
            Result = new Result(null, Id);
            //Result = "Game DRAW";
            return true;
        }

        public UpdateBoard? PlacePieceToBoard(int index,string pieceToPlace)
        {
            // To verify the player's eligibility to play
            if (!IsFirstPlayerPlaying)
            {
                if (Player1.PlayerSign != pieceToPlace) return null;
                IsFirstPlayerPlaying = true;
                Board.Pieces[index] = pieceToPlace;
                return new UpdateBoard(Board.Pieces);
            }
            if (Player2.PlayerSign != pieceToPlace) return null;
            
            IsFirstPlayerPlaying = false;
            Board.Pieces[index] = pieceToPlace;
            return new UpdateBoard(Board.Pieces);
        }
        public Player GetPlayerById(string id)
        {
            return Player1.ConnectionId == id ? Player1 : Player2;
        }

        public void RestartGame()
        {
            Board = new Board();
            Result = null;
        }
    }
}