using DrinkMachine.DAL.Entities;

namespace DrinkMachine.BL.Services.Interfaces;

public interface ICoinService
{
    Task<Coin?> GetCoinByIdAsync(int coinId, CancellationToken ct);
    Task<List<Coin>> GetCoinsListAsync(CancellationToken ct);
    Task<bool> UpdateCoinAsync(Coin coin, CancellationToken ct);
}