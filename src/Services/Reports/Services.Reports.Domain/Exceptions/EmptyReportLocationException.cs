using Shared.Abstractions.Exceptions;

namespace Services.Reports.Domain.Exceptions;

public class EmptyReportLocationException : PhoneDirectoryException
{
    public EmptyReportLocationException() : base("Report location cannot be empty or all white-space characters.")
    {
    }
}