using DrinkMachine.DAL.Entities;
using System.Data;

namespace DrinkMachine.BL.Services.Interfaces;

public interface IDrinkService
{
    Task<List<Drink>> GetAllDrinksAsync(CancellationToken ct);
    Task<Drink?> GetDrinkByIdAsync(int id, CancellationToken ct);
    Task<bool> AddDrinkAsync(Drink drink, CancellationToken ct);
    Task<bool> UpdateDrinkAsync(Drink drink, CancellationToken ct);
    Task<bool> DeleteDrinkAsync(int id, CancellationToken ct);
    Task<bool> SaveChangesAsync(CancellationToken ct);
    IDbTransaction StartTransaction();
}