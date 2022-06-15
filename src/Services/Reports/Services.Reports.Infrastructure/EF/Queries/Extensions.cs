using Services.Reports.Application.DTO;
using Services.Reports.Infrastructure.EF.Models;

namespace Services.Reports.Infrastructure.EF.Queries;

internal static class Extensions
{
    public static ReportDto AsDto(this ReportReadModel readModel)
        => new()
        {
            Location = readModel.Location.ToString(),
            NumberOfContacts = readModel.NumberOfContacts,
            NumberOfPhoneNumbers = readModel.NumberOfPhoneNumbers
        };
}