namespace Services.Reports.Infrastructure.EF.Models;

internal class ReportLocationReadModel
{
    public string Value { get; }

    public ReportLocationReadModel(string value)
    {
        Value = value;
    }

    public override string ToString()
        => Value;
    
    public static implicit operator string(ReportLocationReadModel location)
        => location.Value;
        
    public static implicit operator ReportLocationReadModel(string location)
        => new(location);
}