using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TicTacToe.Domain.Games
{
    public class Player
    {
        [Key]
        public string Id { get; set; }
        public int Rating { get; set; } = 10;
        public string? Name { get; set; }
        public string? ConnectionId { get; set; }
        public string?PlayerSign { get; set; }
        
        [JsonIgnore]
        public Game? Game { get; set; }
    }
}