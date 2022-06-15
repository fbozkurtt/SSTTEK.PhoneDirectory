using Services.Contacts.Domain.Exceptions;

namespace Services.Contacts.Domain.Aggregates.Contact.ValueObjects;

public record ContactLastName
{
    public string Value { get; }

    public ContactLastName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyContactLastNameException();
        }
            
        Value = value;
    }

    public static implicit operator string(ContactLastName name)
        => name.Value;
        
    public static implicit operator ContactLastName?(string? name)
        => name is null ? null : new(name);
}