using Microsoft.EntityFrameworkCore;
using Services.Reports.Application.Services;
using Services.Reports.Infrastructure.EF.Contexts;
using Services.Reports.Infrastructure.EF.Models;

namespace Services.Reports.Infrastructure.EF.Services
{
    internal sealed class PostgresReportReadService : IReportReadService
    {
        private readonly DbSet<ReportReadModel> _reports;

        public PostgresReportReadService(ReadDbContext context)
            => _reports = context.Reports;

        public async Task<Guid?> GetIdByLocationAsync(string location)
        {
            var result = await _reports.Where(r => r.Location == location)
                .Select(r => r.Id)
                .SingleOrDefaultAsync();

            return result == default ? null : result;
        }

        public Task<List<Guid>> GetIdsByLocationsAsync(IEnumerable<string> locations)
        {
            locations = locations.ToList();
            return _reports.Where(r => locations.Any(l => l == r.Location))
                .Select(r => r.Id)
                .ToListAsync();
        }
    }
}