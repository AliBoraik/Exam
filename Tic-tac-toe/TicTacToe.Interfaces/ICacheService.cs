namespace TicTacToe.Interfaces;

public interface ICacheService
{
    Task<bool> SetData(string key, string value);
    Task<string?> GetData(string key);
}