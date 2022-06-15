using Shared.Abstractions.Exceptions;

namespace Services.Reports.Domain.Exceptions;

public class ContactCountLessThanZeroException : PhoneDirectoryException
{
    public ContactCountLessThanZeroException() : base("Contact count cannot be less than zero.")
    {
    }
}