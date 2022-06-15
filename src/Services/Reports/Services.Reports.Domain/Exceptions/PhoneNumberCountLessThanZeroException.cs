using Shared.Abstractions.Exceptions;

namespace Services.Reports.Domain.Exceptions;

public class PhoneNumberCountLessThanZeroException : PhoneDirectoryException
{
    public PhoneNumberCountLessThanZeroException() : base("Phone number count cannot be less than zero.")
    {
    }
}