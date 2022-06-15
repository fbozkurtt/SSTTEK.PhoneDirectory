using Shared.Abstractions.Exceptions;

namespace Services.Contacts.Domain.Exceptions;

public class InvalidEmailFormatException : PhoneDirectoryException
{
    public InvalidEmailFormatException(string email) : base($"Value '{email}' is not a valid email format.")
    {
        Email = email;
    }

    public string Email { get; }
}