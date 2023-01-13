using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TicTacToe.Domain.Games
{
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public string ConnectionId { get; set; }
        public string PlayerSign { get; set; }
    }
}