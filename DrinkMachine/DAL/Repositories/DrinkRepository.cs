using DrinkMachine.DAL.Data;
using DrinkMachine.DAL.Entities;
using DrinkMachine.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DrinkMachine.DAL.Repositories;

public class DrinkRepository : IDrinkRepository
{
    private readonly AppDbContext _context;

    public DrinkRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Drink>> GetAllAsync(CancellationToken ct)
    {
        return await _context.Drinks.ToListAsync(ct);
    }

    public async Task<Drink?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await _context.Drinks.FindAsync(new object?[] { id, ct }, cancellationToken: ct);
    }

    public async Task AddAsync(Drink entity, CancellationToken ct)
    {
        await _context.Drinks.AddAsync(entity, ct);
    }

    public void Update(Drink entity)
    {
        _context.Drinks.Update(entity);
    }

    public void Delete(Drink drink)
    {
        _context.Drinks.Remove(drink);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken ct)
    {
        return await _context.SaveChangesAsync(ct) > 0;
    }
}