using Shared.Abstractions.Exceptions;

namespace Services.Contacts.Domain.Exceptions;

public class EmptyContactIdException : PhoneDirectoryException
{
    public EmptyContactIdException() : base("Contact identifier cannot be empty.")
    {
    }
}