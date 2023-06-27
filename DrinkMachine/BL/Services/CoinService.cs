using DrinkMachine.BL.Services.Interfaces;
using DrinkMachine.DAL.Entities;
using DrinkMachine.DAL.Repositories.Interfaces;

namespace DrinkMachine.BL.Services;

public class CoinService : ICoinService
{
    private readonly ICoinRepository _coinRepository;

    public CoinService(ICoinRepository coinRepository)
    {
        _coinRepository = coinRepository;
    }

    public async Task<Coin?> GetCoinByIdAsync(int coinId, CancellationToken ct)
    {
        if (coinId <= 0)
            return null;

        return await _coinRepository.GetCoinByIdAsync(coinId, ct);
    }

    public async Task<List<Coin>> GetCoinsListAsync(CancellationToken ct)
    {
        return await _coinRepository.GetCoinsListAsync(ct);
    }

    public async Task<bool> UpdateCoinAsync(Coin coin, CancellationToken ct)
    {
        _coinRepository.UpdateCoin(coin);
        var result = await _coinRepository.SaveChangesAsync(ct);
        return result;
    }
}