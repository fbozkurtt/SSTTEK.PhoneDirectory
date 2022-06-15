using Services.Reports.Domain.Entities.Report;
using Services.Reports.Domain.Entities.Report.ValueObjects;

namespace Services.Reports.Domain.Factories;

public class ReportFactory : IReportFactory
{
    public Report Create(ReportId id, ReportLocation location)
        => new(id, location);
}