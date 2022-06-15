using Services.Contacts.Domain.Aggregates.Contact.ValueObjects;
using Shared.Abstractions.Domain;

namespace Services.Contacts.Domain.Aggregates.Contact.Events;

public record ContactFieldRemoved(ContactField Field, IEnumerable<ContactField> Fields) : IDomainEvent;