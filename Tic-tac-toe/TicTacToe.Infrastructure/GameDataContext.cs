using Microsoft.EntityFrameworkCore;
using TicTacToe.Domain.Games;

namespace TicTacToe.Infrastructure;

public class GameDataContext : DbContext
{
    public GameDataContext() {}
    public GameDataContext(DbContextOptions options) : base(options) {}
    
    public DbSet<Game> Games { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
        base.OnModelCreating(modelBuilder);
    }
    
}