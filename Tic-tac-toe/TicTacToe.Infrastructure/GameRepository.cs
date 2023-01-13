using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Domain.Enum;
using TicTacToe.Domain.Games;
using TicTacToe.Interfaces;

namespace TicTacToe.Infrastructure;

public class GameRepository : IGameRepository
{
    private readonly GameDataContext _ctx;
    private readonly UserManager<IdentityUser> userManager;

    public GameRepository(GameDataContext context, UserManager<IdentityUser> userManager)
    {
        _ctx = context;
        this.userManager = userManager;
    }

    public async Task<List<Game>> GetAllGames()
    {
        return await _ctx.Games.Include(g => g.Players).ToListAsync();
    }

    public async Task<Game?> GetGame(string id)
    {
        return await _ctx.Games.FirstOrDefaultAsync(game => game.Id == id)!;
    }

    public async Task CreateGame(Game game)
    {
        await _ctx.Games.AddAsync(game);
        await _ctx.SaveChangesAsync();
    }
    public async Task<bool> UpdateGame(Game? game)
    {
        try
        {
            var ctxGame = await _ctx.Games.FindAsync(game.Id);
            if (ctxGame == null) return false;
            _ctx.Games.Update(ctxGame);
            await _ctx.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<Player?> FindPlayer(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user != null)
        {
            Player? player = await _ctx.Players.FindAsync(user.Id);
            return player;
        }
        return null;
    }
    
    public async Task<Game?> FindFreeGame()
    {
        var g = await _ctx.Games.FirstOrDefaultAsync(g => g.Status == GameStatus.Waiting);
        return g;
    }

    public async Task CreatePlayer(Player? player)
    {
         await _ctx.Players.AddAsync(player);
         await _ctx.SaveChangesAsync();
    }
}