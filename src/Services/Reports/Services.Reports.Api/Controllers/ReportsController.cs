using Microsoft.AspNetCore.Mvc;
using Services.Reports.Application.DTO;
using Services.Reports.Application.Queries;
using Shared.Abstractions.Queries;

namespace Services.Reports.Api.Controllers;

public class ReportsController : BaseController
{
    private readonly IQueryDispatcher _queryDispatcher;

    public ReportsController(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReportDto>>> Get()
    {
        var result = await _queryDispatcher.QueryAsync(new GetReports());
        return OkOrNotFound(result);
    }
}