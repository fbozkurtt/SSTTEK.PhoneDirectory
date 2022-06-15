using MassTransit;
using Shared.Abstractions.Domain;
using Shared.Abstractions.Events;

namespace Services.Contacts.Infrastructure.Events;

public class MassTransitDomainEventPublisher : IDomainEventPublisher
{
    private readonly IBus _eventBus;

    public MassTransitDomainEventPublisher(IBus eventBus)
    {
        _eventBus = eventBus;
    }

    public Task PublishAsync<TEvent>(params TEvent[] events) where TEvent : class, IDomainEvent
    {
        var publishEventTasks = events.Select(@event => _eventBus.Publish(@event, @event.GetType())).ToList();

        return Task.WhenAll(publishEventTasks);
    }
}