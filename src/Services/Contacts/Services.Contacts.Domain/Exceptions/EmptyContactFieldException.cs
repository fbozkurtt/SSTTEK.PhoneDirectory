using Shared.Abstractions.Exceptions;

namespace Services.Contacts.Domain.Exceptions;

public class EmptyContactFieldException : PhoneDirectoryException
{
    public EmptyContactFieldException() : base("Contact field cannot be empty or all white-space characters.")
    {
    }
}