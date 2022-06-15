using Microsoft.EntityFrameworkCore;
using Services.Reports.Domain.Entities.Report;
using Services.Reports.Domain.Entities.Report.ValueObjects;
using Services.Reports.Infrastructure.EF.Contexts;

namespace Services.Reports.Infrastructure.EF.Repositories;

internal sealed class PostgresReportRepository : IReportRepository
{
    private readonly DbSet<Report> _reports;
    private readonly WriteDbContext _writeDbContext;

    public PostgresReportRepository(WriteDbContext writeDbContext)
    {
        _reports = writeDbContext.Reports;
        _writeDbContext = writeDbContext;
    }

    public Task<Report?> GetAsync(ReportId id)
    {
        return _reports.AsNoTracking().SingleOrDefaultAsync(r => r.Id == id);
    }

    public Task<List<Report>> GetAsync(IEnumerable<ReportId> ids)
    {
        return _reports.AsNoTracking().Where(r => ids.Contains(r.Id)).ToListAsync();
    }

    public async Task AddAsync(Report report)
    {
        await _reports.AddAsync(report);
        await _writeDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Report report)
    {
        _reports.Update(report);
        await _writeDbContext.SaveChangesAsync();
    }

    public async Task UpdateRangeAsync(IEnumerable<Report> reports)
    {
        _reports.UpdateRange(reports);
        await _writeDbContext.SaveChangesAsync();
    }
}