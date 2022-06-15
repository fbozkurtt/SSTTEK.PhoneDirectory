using Shared.Abstractions.Exceptions;

namespace Services.Reports.Domain.Exceptions;

public class EmptyReportIdException : PhoneDirectoryException
{
    public EmptyReportIdException() : base("Report identifier cannot be empty.")
    {
    }
}