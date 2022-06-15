using Shared.Abstractions.Exceptions;

namespace Services.Contacts.Domain.Exceptions;

public class InvalidPhoneNumberFormatException : PhoneDirectoryException
{
    public string PhoneNumber { get; }

    public InvalidPhoneNumberFormatException(string phoneNumber) : base($"Value '{phoneNumber}' is not a valid phone number format.")
        => PhoneNumber = phoneNumber;
}