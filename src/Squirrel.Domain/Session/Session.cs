using Squirrel.Domain.Abstractions;

namespace Squirrel.Domain.Session;

public class Session : Entity
{
    public Session(Guid id, Duration duration, IsFinished isFinished, Type type, Guid userId) : base(id)
    {
        Duration = duration;
        IsFinished = isFinished;
        Type = type;
        UserId = userId;
    }
    public Guid UserId { get; private set; }
    
    public Duration Duration { get; private set; }
    
    public IsFinished IsFinished { get; private set; }
    
    public Type Type { get; private set; }
    
    public static Session Create(Guid userId, Duration duration, IsFinished isFinished, Type type)
    {
        var session = new Session(
            Guid.NewGuid(),
            duration,
            isFinished,
            type,
            userId);
        
        return session;
    }
}