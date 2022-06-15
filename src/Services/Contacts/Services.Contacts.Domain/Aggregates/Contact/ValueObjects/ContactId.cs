using Services.Contacts.Domain.Exceptions;

namespace Services.Contacts.Domain.Aggregates.Contact.ValueObjects;

public record ContactId
{
    public ContactId(Guid value)
    {
        if (value == Guid.Empty) throw new EmptyContactIdException();

        Value = value;
    }

    public Guid Value { get; }

    public static implicit operator Guid(ContactId id)
    {
        return id.Value;
    }

    public static implicit operator ContactId(Guid id)
    {
        return new(id);
    }
}