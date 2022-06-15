using Shared.Abstractions.Exceptions;

namespace Services.Contacts.Domain.Exceptions;

public class EmptyContactFirstNameException : PhoneDirectoryException
{
    public EmptyContactFirstNameException() : base("Contact first name cannot be empty or all white-space characters.")
    {
    }
}