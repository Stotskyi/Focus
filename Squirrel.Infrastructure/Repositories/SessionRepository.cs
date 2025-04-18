using Microsoft.EntityFrameworkCore;
using Squirrel.Domain.Session;

namespace Squirrel.Infrastructure.Repositories;

internal sealed class SessionRepository : Repository<Session>, ISessionRepository
{
    public SessionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
    
    public override void Add(Session session)
    {
        DbContext.Add(session);
    }
}