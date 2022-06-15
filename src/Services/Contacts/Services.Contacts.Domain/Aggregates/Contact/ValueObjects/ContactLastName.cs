using Services.Contacts.Domain.Exceptions;

namespace Services.Contacts.Domain.Aggregates.Contact.ValueObjects;

public record ContactLastName
{
    public ContactLastName(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new EmptyContactLastNameException();

        Value = value;
    }

    public string Value { get; }

    public static implicit operator string(ContactLastName name)
    {
        return name.Value;
    }

    public static implicit operator ContactLastName?(string? name)
    {
        return name is null ? null : new ContactLastName(name);
    }
}