using Services.Reports.Domain.Entities.Report.ValueObjects;

namespace Services.Reports.Domain.Entities.Report;

public interface IReportRepository
{
    Task<Report?> GetAsync(ReportId id);
    Task<List<Report>> GetAsync(IEnumerable<ReportId> ids);
    Task AddAsync(Report report);
    Task UpdateAsync(Report report);
    Task UpdateRangeAsync(IEnumerable<Report> reports);
}