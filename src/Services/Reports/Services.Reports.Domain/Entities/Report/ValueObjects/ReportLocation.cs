using Services.Reports.Domain.Exceptions;

namespace Services.Reports.Domain.Entities.Report.ValueObjects;

public record ReportLocation
{
    public string Value { get; }

    public ReportLocation(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyReportLocationException();
        }
            
        Value = value;
    }

    public static implicit operator string(ReportLocation location)
        => location.Value;
        
    public static implicit operator ReportLocation(string location)
        => new(location);
}