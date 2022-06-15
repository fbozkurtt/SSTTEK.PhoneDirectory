using Services.Reports.Domain.Exceptions;

namespace Services.Reports.Domain.Entities.Report.ValueObjects;

public record ReportId
{
    public ReportId(Guid value)
    {
        if (value == Guid.Empty) throw new EmptyReportIdException();

        Value = value;
    }

    public Guid Value { get; }

    public static implicit operator Guid(ReportId id)
    {
        return id.Value;
    }

    public static implicit operator ReportId(Guid id)
    {
        return new(id);
    }
}