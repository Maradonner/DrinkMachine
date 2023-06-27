using DrinkMachine.DAL.Entities;

namespace DrinkMachine.DAL.Repositories.Interfaces;

public interface IDbSessionRepository
{
    Task<SessionModel?> Get(Guid sessionId);

    Task UpdateBalance(Guid dbSessionId, int balance);

    Task Extend(Guid dbSessionId);

    Task Create(SessionModel model);

    Task Lock(Guid sessionId);
}