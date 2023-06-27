using DrinkMachine.DAL.Data;
using DrinkMachine.DAL.Entities;
using DrinkMachine.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DrinkMachine.DAL.Repositories;

public class CoinRepository : ICoinRepository
{
    private readonly AppDbContext _context;

    public CoinRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Coin?> GetCoinByIdAsync(int coinId, CancellationToken ct)
    {
        return await _context.Coins.FindAsync(new object?[] { coinId, ct }, cancellationToken: ct);
    }

    public async Task<List<Coin>> GetCoinsListAsync(CancellationToken ct)
    {
        return await _context.Coins.ToListAsync(ct);
    }

    public void UpdateCoin(Coin coin)
    {
        _context.Coins.Update(coin);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken ct)
    {
        return await _context.SaveChangesAsync(ct) > 0;
    }
}