using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Domain.Games;

namespace TicTacToe.Infrastructure;

public class GameDataContext :IdentityDbContext<IdentityUser>
{

    public GameDataContext(DbContextOptions<GameDataContext> options) : base(options)  
    {  
  
    }
    public DbSet<Game> Games { get; set; } = null!;
    public DbSet<Player?> Players { get; set; } = null!;
    
}