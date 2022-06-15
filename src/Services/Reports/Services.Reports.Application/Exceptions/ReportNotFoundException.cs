using Shared.Abstractions.Exceptions;

namespace Services.Reports.Application.Exceptions;

public class ReportNotFoundException : PhoneDirectoryException
{
    public Guid Id { get; }

    public ReportNotFoundException(Guid id) : base($"Report with the identifier '{id}' could not be found.")
        => Id = id;
}