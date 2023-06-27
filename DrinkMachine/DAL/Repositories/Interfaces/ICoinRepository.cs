using DrinkMachine.DAL.Entities;

namespace DrinkMachine.DAL.Repositories.Interfaces;

public interface ICoinRepository
{
    Task<Coin?> GetCoinByIdAsync(int coinId, CancellationToken ct);
    Task<List<Coin>> GetCoinsListAsync(CancellationToken ct);
    void UpdateCoin(Coin coin);
    Task<bool> SaveChangesAsync(CancellationToken ct);
}