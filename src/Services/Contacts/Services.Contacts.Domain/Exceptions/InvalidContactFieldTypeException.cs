using Services.Contacts.Domain.Enums;
using Shared.Abstractions.Exceptions;

namespace Services.Contacts.Domain.Exceptions;

public class InvalidContactFieldTypeException : PhoneDirectoryException
{
    public ContactFieldType Type { get; }

    public InvalidContactFieldTypeException(ContactFieldType type) : base($"Field type '{type}' is invalid.")
        => Type = type;
}