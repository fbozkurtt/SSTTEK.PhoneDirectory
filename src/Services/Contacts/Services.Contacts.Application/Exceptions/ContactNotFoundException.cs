using Shared.Abstractions.Exceptions;

namespace Services.Contacts.Application.Exceptions;

public class ContactNotFoundException : PhoneDirectoryException
{
    public Guid Id { get; }

    public ContactNotFoundException(Guid id) : base($"Contact with the identifier '{id}' could not be found.")
        => Id = id;
}