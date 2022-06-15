namespace Services.Reports.Infrastructure.EF.Models;

internal class ReportLocationReadModel
{
    public ReportLocationReadModel(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public override string ToString()
    {
        return Value;
    }

    public static implicit operator string(ReportLocationReadModel location)
    {
        return location.Value;
    }

    public static implicit operator ReportLocationReadModel(string location)
    {
        return new(location);
    }
}