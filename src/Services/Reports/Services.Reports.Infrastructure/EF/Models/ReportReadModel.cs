namespace Services.Reports.Infrastructure.EF.Models;

internal class ReportReadModel
{
    public Guid Id { get; set; }
    public uint NumberOfContacts { get; set; }
    public uint NumberOfPhoneNumbers { get; set; }
    public ReportLocationReadModel Location { get; set; } = null!;
}