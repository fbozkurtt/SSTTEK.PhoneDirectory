namespace Services.Reports.Application.DTO;

public class ReportDto
{
    public string Location { get; set; } = null!;
    public uint NumberOfContacts { get; set; }
    public uint NumberOfPhoneNumbers { get; set; }
}