using Services.Reports.Domain.Entities.Report;
using Services.Reports.Domain.Entities.Report.ValueObjects;

namespace Services.Reports.Domain.Factories;

public interface IReportFactory
{
    Report Create(ReportId id, ReportLocation location);
}