namespace Services.Reports.Application.Services;

public interface IReportReadService
{
    Task<Guid?> GetIdByLocationAsync(string location);
    Task<List<Guid>> GetIdsByLocationsAsync(IEnumerable<string> locations);
}