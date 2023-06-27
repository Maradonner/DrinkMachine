using DrinkMachine.DAL.Entities;

namespace DrinkMachine.BL.Services.Interfaces;

public interface IDbSessionService
{
    Task<SessionModel> GetSessionAsync();
    Task LockAsync();
    void ResetSessionCache();
    Task UpdateBalanceAsync(int value);
}