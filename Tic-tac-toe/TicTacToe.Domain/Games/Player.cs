namespace TicTacToe.Domain.Games
{
    public class Player
    {
        public string PlayerId { get; set; }

        public int Rating { get; set; } = 10;
        public string Name { get; set; }
        public string? ConnectionId { get; set; }
        public string?PlayerSign { get; set; }
    }
}