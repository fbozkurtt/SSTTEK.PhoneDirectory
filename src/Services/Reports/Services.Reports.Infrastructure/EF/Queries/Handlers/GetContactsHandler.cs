using Microsoft.EntityFrameworkCore;
using Services.Reports.Application.DTO;
using Services.Reports.Application.Queries;
using Services.Reports.Infrastructure.EF.Contexts;
using Services.Reports.Infrastructure.EF.Models;
using Shared.Abstractions.Queries;

namespace Services.Reports.Infrastructure.EF.Queries.Handlers;

internal sealed class GetReportsHandler : IQueryHandler<GetReports, IEnumerable<ReportDto>>
{
    private readonly DbSet<ReportReadModel> _reports;

    public GetReportsHandler(ReadDbContext dbContext)
        => _reports = dbContext.Reports;

    public async Task<IEnumerable<ReportDto>> HandleAsync(GetReports query)
        => await _reports
            .AsNoTracking()
            .OrderByDescending(r => r.NumberOfContacts)
            .Select(r => r.AsDto())
            .ToListAsync();
}