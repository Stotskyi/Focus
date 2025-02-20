using Microsoft.EntityFrameworkCore;
using Squirrel.Domain.Session;

namespace Squirrel.Infrastructure.Repositories;

internal sealed class SessionRepository : Repository<Session>, ISessionRepository
{
    public SessionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IQueryable<Session>> GetAllSessionsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(DbContext.Set<Session>().Where(session => session.UserId == id).AsQueryable().AsNoTracking());
    }

    public override void Add(Session session)
    {
        DbContext.Add(session);
    }
}