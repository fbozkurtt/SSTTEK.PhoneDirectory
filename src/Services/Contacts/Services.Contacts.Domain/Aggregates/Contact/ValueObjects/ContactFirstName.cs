using Services.Contacts.Domain.Exceptions;

namespace Services.Contacts.Domain.Aggregates.Contact.ValueObjects;

public record ContactFirstName
{
    public ContactFirstName(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new EmptyContactFirstNameException();

        Value = value;
    }

    public string Value { get; }

    public static implicit operator string(ContactFirstName name)
    {
        return name.Value;
    }

    public static implicit operator ContactFirstName(string name)
    {
        return new(name);
    }
}