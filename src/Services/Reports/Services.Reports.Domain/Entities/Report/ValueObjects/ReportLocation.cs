using Services.Reports.Domain.Exceptions;

namespace Services.Reports.Domain.Entities.Report.ValueObjects;

public record ReportLocation
{
    public ReportLocation(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new EmptyReportLocationException();

        Value = value;
    }

    public string Value { get; }

    public static implicit operator string(ReportLocation location)
    {
        return location.Value;
    }

    public static implicit operator ReportLocation(string location)
    {
        return new(location);
    }
}