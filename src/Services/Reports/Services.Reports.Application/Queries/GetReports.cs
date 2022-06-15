using Services.Reports.Application.DTO;
using Shared.Abstractions.Queries;

namespace Services.Reports.Application.Queries;

public class GetReports : IQuery<IEnumerable<ReportDto>>
{
}