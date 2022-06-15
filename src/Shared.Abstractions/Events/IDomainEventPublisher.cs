using Shared.Abstractions.Domain;

namespace Shared.Abstractions.Events;

public interface IDomainEventPublisher
{
    Task PublishAsync<TEvent>(params TEvent[] events) where TEvent : class, IDomainEvent;
}