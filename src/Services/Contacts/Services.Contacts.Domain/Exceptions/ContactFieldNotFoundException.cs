using Services.Contacts.Domain.Enums;
using Shared.Abstractions.Exceptions;

namespace Services.Contacts.Domain.Exceptions;

public class ContactFieldNotFoundException : PhoneDirectoryException
{
    public string Value { get; }
    public ContactFieldType Type { get; }

    public ContactFieldNotFoundException(string value, ContactFieldType type)
        : base($"Contact field with the value of '{value}' and type of '{type}' could not be found.")
    {
        Value = value;
        Type = type;
    }
}