using TicTacToe.Domain.Enum;
using static System.Guid;

namespace TicTacToe.Domain.Games
{
    public class Game
    {
        public string Id { get; set; }
        public List<Player?> Players { get; set; }
        
        public Board Board;
        public bool IsFirstPlayersTurn;
        public GameStatus Status { get; set; }
        private readonly int[,] _winCombination;
        public Result? Result;
        private bool IsFirstPlayerPlaying;

        public Game()
        {
            Id = NewGuid().ToString("d");
            Status = GameStatus.Waiting;
            IsFirstPlayersTurn = true;
            Board = new Board();
            Players = new List<Player?>();
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
                if (Board.Pieces[_winCombination[i, 0]] == Players[0].PlayerSign
                    && Board.Pieces[_winCombination[i, 1]] == Players[0].PlayerSign
                    && Board.Pieces[_winCombination[i, 2]] == Players[0].PlayerSign)
                {
                    Result = new Result(Players[0], Id);
                    //Result = $"{Player1.Name} WON";
                    return true;
                }
                //* O win check
                if (Board.Pieces[_winCombination[i, 0]] == Players[1].PlayerSign
                    && Board.Pieces[_winCombination[i, 1]] == Players[1].PlayerSign
                    && Board.Pieces[_winCombination[i, 2]] == Players[1].PlayerSign)
                {
                    Result = new Result(Players[1], Id);
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
                if (Players[0].PlayerSign != pieceToPlace) return null;
                IsFirstPlayerPlaying = true;
                Board.Pieces[index] = pieceToPlace;
                return new UpdateBoard(Board.Pieces);
            }
            if (Players[1].PlayerSign != pieceToPlace) return null;
            
            IsFirstPlayerPlaying = false;
            Board.Pieces[index] = pieceToPlace;
            return new UpdateBoard(Board.Pieces);
        }
        public Player? GetPlayerById(string id)
        {
            return Players[0].ConnectionId == id ? Players[0] : Players[1];
        }

        public void RestartGame()
        {
            Board = new Board();
            Result = null;
            Status = GameStatus.InProgress;
        }
    }
}