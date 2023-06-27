using DrinkMachine.BL.Constants;
using DrinkMachine.BL.Cookie;
using DrinkMachine.BL.Services.Interfaces;
using DrinkMachine.DAL.Entities;
using DrinkMachine.DAL.Repositories.Interfaces;

namespace DrinkMachine.BL.Services;

public class DbSessionService : IDbSessionService
{
    private readonly IDbSessionRepository _sessionDAL;
    private readonly IWebCookie _webCookie;
    private SessionModel? _sessionModel;


    public DbSessionService(IDbSessionRepository sessionDAL, IWebCookie webCookie)
    {
        _sessionDAL = sessionDAL;
        _webCookie = webCookie;
    }

    public async Task<SessionModel> GetSessionAsync()
    {
        if (_sessionModel != null)
            return _sessionModel;

        Guid sessionId;
        var sessionString = _webCookie.Get(AuthConstants.SessionCookieName);
        if (sessionString != null)
            sessionId = Guid.Parse(sessionString);
        else
            sessionId = Guid.NewGuid();

        var data = await _sessionDAL.Get(sessionId);
        if (data == null)
        {
            data = await CreateSessionAsync();
            CreateSessionCookie(data.Id);
        }

        _sessionModel = data;


        await _sessionDAL.Extend(data.Id);
        return data;
    }

    public async Task UpdateBalanceAsync(int value)
    {
        if (_sessionModel != null)
            await _sessionDAL.UpdateBalance(_sessionModel.Id, value);
        else
            throw new Exception("Сессия не загружена");
    }

    public async Task LockAsync()
    {
        var data = await GetSessionAsync();
        await _sessionDAL.Lock(data.Id);
    }

    public void ResetSessionCache()
    {
        _sessionModel = null;
    }

    private void CreateSessionCookie(Guid sessionid)
    {
        _webCookie.Delete(AuthConstants.SessionCookieName);
        _webCookie.AddSecure(AuthConstants.SessionCookieName, sessionid.ToString());
    }

    private async Task<SessionModel> CreateSessionAsync()
    {
        var data = new SessionModel
        {
            Id = Guid.NewGuid(),
            Created = DateTime.Now,
            LastAccessed = DateTime.Now
        };
        await _sessionDAL.Create(data);
        return data;
    }
}