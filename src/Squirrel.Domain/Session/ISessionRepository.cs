namespace Squirrel.Domain.Session;

public interface ISessionRepository
{
    public Task<IQueryable<Session>> GetAllSessionsAsync(Guid id, CancellationToken cancellationToken = default);
    public void Add(Session session);
}