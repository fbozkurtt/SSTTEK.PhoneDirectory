using Services.Contacts.Domain.Aggregates.Contact.ValueObjects;
using Shared.Abstractions.Exceptions;

namespace Services.Contacts.Domain.Exceptions;

public class ContactFieldAlreadyExistsException : PhoneDirectoryException
{
    public ContactField Field { get; }

    public ContactFieldAlreadyExistsException(ContactField field)
        : base($"Field '{field.Value}' with the type of '{field.Type}' already exists within the contact.")
    {
        Field = field;
    }
}