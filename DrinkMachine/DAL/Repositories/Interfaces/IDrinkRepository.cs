using DrinkMachine.DAL.Entities;

namespace DrinkMachine.DAL.Repositories.Interfaces;

public interface IDrinkRepository
{
    Task<List<Drink>> GetAllAsync(CancellationToken ct);
    Task<Drink?> GetByIdAsync(int id, CancellationToken ct);
    Task AddAsync(Drink entity, CancellationToken ct);
    void Update(Drink entity);
    void Delete(Drink drink);
    Task<bool> SaveChangesAsync(CancellationToken ct);
}