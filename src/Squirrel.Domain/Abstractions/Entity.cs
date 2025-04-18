namespace Squirrel.Domain.Abstractions;

public abstract class Entity
{
    public Guid Id { get; protected set; }
    
    private readonly List<IDomainEvent> _domainEvents = new();

    protected Entity(Guid id)
    {
        Id = id;
    } 
    
    protected Entity()
    {
    }
    public IReadOnlyList<IDomainEvent> GetDomainEvents() => _domainEvents.ToList();
    
    public void ClearDomainEvents() => _domainEvents.Clear();
    
    protected void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}