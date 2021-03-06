using Shared.Abstractions.Exceptions;

namespace Services.Contacts.Domain.Exceptions;

public class InvalidPhoneNumberFormatException : PhoneDirectoryException
{
    public InvalidPhoneNumberFormatException(string phoneNumber) : base(
        $"Value '{phoneNumber}' is not a valid phone number format.")
    {
        PhoneNumber = phoneNumber;
    }

    public string PhoneNumber { get; }
}