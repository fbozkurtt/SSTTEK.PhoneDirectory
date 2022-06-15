using Services.Contacts.Domain.Exceptions;

namespace Services.Contacts.Domain.Aggregates.Contact.ValueObjects;

public record ContactFirstName
{
    public string Value { get; }

    public ContactFirstName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyContactFirstNameException();
        }
            
        Value = value;
    }

    public static implicit operator string(ContactFirstName name)
        => name.Value;
        
    public static implicit operator ContactFirstName(string name)
        => new(name);
}