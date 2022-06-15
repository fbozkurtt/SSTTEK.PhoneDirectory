using System.Text.Json.Serialization;

namespace Shared.Abstractions.Domain;

public abstract class AggregateRoot<T>
{
    private readonly List<IDomainEvent> _events = new();
    private bool _versionIncremented;
    public T Id { get; protected set; } = default!;
    public int Version { get; protected set; }

    [JsonIgnore] public IEnumerable<IDomainEvent> Events => _events;

    protected void AddEvent(IDomainEvent @event)
    {
        if (!_events.Any() && !_versionIncremented)
        {
            Version++;
            _versionIncremented = true;
        }

        _events.Add(@event);
    }

    public void ClearEvents()
    {
        _events.Clear();
    }
}