using Squirrel.Domain.Abstractions;
using Squirrel.Domain.Session.Events;

namespace Squirrel.Domain.Session;

public class Session : Entity
{
    private Session(Guid id, Duration duration, IsFinished isFinished, Type type, Guid userId) : base(id)
    {
        Duration = duration;
        IsFinished = isFinished;
        Type = type;
        UserId = userId;
    }

    private Session()
    {
        
    }
    public Guid UserId { get; private set; }
    
    public Duration Duration { get; private set; }
    
    public IsFinished IsFinished { get; private set; }
    
    public Type Type { get; private set; }
    
    public  static Session Create(Guid userId, Duration duration, IsFinished isFinished, Type type)
    {
        var session = new Session(
            Guid.NewGuid(),
            duration,
            isFinished,
            type,
            userId);

        session.RaiseDomainEvent(new SessionCreateDomainEvent(session.UserId, session.Duration.Value));
        
        return session;
    }
}