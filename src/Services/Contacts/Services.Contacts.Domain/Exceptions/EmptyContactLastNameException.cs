using Shared.Abstractions.Exceptions;

namespace Services.Contacts.Domain.Exceptions;

public class EmptyContactLastNameException : PhoneDirectoryException
{
    public EmptyContactLastNameException() : base("Contact last name cannot be empty or all white-space characters.")
    {
    }
}