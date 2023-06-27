using System.Data;
using DrinkMachine.BL.Services.Interfaces;
using DrinkMachine.DAL.Entities;
using DrinkMachine.DAL.Repositories.Interfaces;

namespace DrinkMachine.BL.Services;

public class DrinkService : IDrinkService
{
    private readonly IDrinkRepository _drinkRepository;

    public DrinkService(IDrinkRepository drinkRepository)
    {
        _drinkRepository = drinkRepository;
    }

    public async Task<List<Drink>> GetAllDrinksAsync(CancellationToken ct)
    {
        var drinks = await _drinkRepository.GetAllAsync(ct);
        return drinks;
    }

    public async Task<Drink?> GetDrinkByIdAsync(int id, CancellationToken ct)
    {
        return await _drinkRepository.GetByIdAsync(id, ct);
    }

    public async Task<bool> AddDrinkAsync(Drink drink, CancellationToken ct)
    {
        await _drinkRepository.AddAsync(drink, ct);
        return await _drinkRepository.SaveChangesAsync(ct);
    }

    public async Task<bool> DeleteDrinkAsync(int id, CancellationToken ct)
    {
        var drink = await GetDrinkByIdAsync(id, ct);

        if (drink == null)
            return false;
        
        _drinkRepository.Delete(drink);
        return await _drinkRepository.SaveChangesAsync(ct);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken ct)
    {
        return await _drinkRepository.SaveChangesAsync(ct);
    }

    public IDbTransaction StartTransaction()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateDrinkAsync(Drink drink, CancellationToken ct)
    {
        _drinkRepository.Update(drink);
        return await _drinkRepository.SaveChangesAsync(ct);
    }
}