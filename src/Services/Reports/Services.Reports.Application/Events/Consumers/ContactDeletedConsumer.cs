using MassTransit;
using Services.Contacts.Domain.Aggregates.Contact.Events;
using Services.Contacts.Domain.Enums;
using Services.Reports.Application.Services;
using Services.Reports.Domain.Entities.Report;
using Services.Reports.Domain.Entities.Report.ValueObjects;

namespace Services.Reports.Application.Events.Consumers;

public class ContactDeletedConsumer : IConsumer<ContactDeleted>
{
    private readonly IReportRepository _reportRepository;
    private readonly IReportReadService _reportReadService;

    public ContactDeletedConsumer(IReportRepository reportRepository, IReportReadService reportReadService)
    {
        _reportRepository = reportRepository;
        _reportReadService = reportReadService;
    }

    public async Task Consume(ConsumeContext<ContactDeleted> context)
    {
        var affectedLocations = context.Message.Fields
            .Where(f => f.Type == ContactFieldType.Location)
            .Select(f => f.Value).ToArray();

        if (affectedLocations.Length is 0)
            return;

        var reportIds = (await _reportReadService.GetIdsByLocationsAsync(affectedLocations))
            .Select(id => new ReportId(id)).ToList();

        var reportsToUpdate = await _reportRepository.GetAsync(reportIds);
        var phoneNumberCount = context.Message.Fields.Count(f => f.Type == ContactFieldType.Phone);
        
        foreach (var report in reportsToUpdate)
        {
            report.DecrementPhoneNumberCount((uint) phoneNumberCount);
            report.DecrementContactCount();
        }

        await _reportRepository.UpdateRangeAsync(reportsToUpdate.ToArray());
    }
}