using DrinkMachine.DAL.Data;
using DrinkMachine.DAL.Entities;
using DrinkMachine.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DrinkMachine.DAL.Repositories;

public class DbSessionRepository : IDbSessionRepository
{
    private readonly AppDbContext _context;

    public DbSessionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Create(SessionModel model)
    {
        await _context.DbSessions.AddAsync(model);
        await _context.SaveChangesAsync();
    }

    public async Task<SessionModel?> Get(Guid sessionId)
    {
        return await _context.DbSessions.FindAsync(sessionId);
    }

    public async Task Lock(Guid sessionId)
    {
        var session = await _context.DbSessions.FindAsync(sessionId);
        if (session != null) _context.Entry(session).State = EntityState.Modified;
    }

    public async Task UpdateBalance(Guid dbSessionId, int balance)
    {
        var session = await _context.DbSessions.FindAsync(dbSessionId);
        if (session != null)
        {
            session.Balance += balance;
            await _context.SaveChangesAsync();
        }
    }

    public async Task Extend(Guid dbSessionId)
    {
        var session = await _context.DbSessions.FindAsync(dbSessionId);
        if (session != null)
        {
            session.LastAccessed = DateTime.Now;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}