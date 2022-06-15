using MassTransit;
using Services.Contacts.Domain.Aggregates.Contact.Events;
using Services.Contacts.Domain.Enums;
using Services.Reports.Application.Exceptions;
using Services.Reports.Application.Services;
using Services.Reports.Domain.Entities.Report;
using Services.Reports.Domain.Entities.Report.ValueObjects;

namespace Services.Reports.Application.Events.Consumers;

public class ContactFieldRemovedConsumer : IConsumer<ContactFieldRemoved>
{
    private readonly IReportReadService _reportReadService;
    private readonly IReportRepository _reportRepository;

    public ContactFieldRemovedConsumer(IReportRepository reportRepository, IReportReadService reportReadService)
    {
        _reportRepository = reportRepository;
        _reportReadService = reportReadService;
    }

    public async Task Consume(ConsumeContext<ContactFieldRemoved> context)
    {
        var field = context.Message.Field;

        switch (field.Type)
        {
            case ContactFieldType.Location:
            {
                var location = field.Value;

                var reportId = await _reportReadService.GetIdByLocationAsync(location);

                if (reportId is null)
                    return;

                var report = await _reportRepository.GetAsync(reportId);
                if (report is null)
                    throw new ReportNotFoundException(reportId.GetValueOrDefault());

                var phoneNumberCount = context.Message.Fields.Count(f => f.Type is ContactFieldType.Phone);
                report.DecrementContactCount();
                report.DecrementPhoneNumberCount((uint) phoneNumberCount);

                await _reportRepository.UpdateAsync(report);

                break;
            }
            case ContactFieldType.Phone:
            {
                var affectedLocations = context.Message.Fields
                    .Where(f => f.Type == ContactFieldType.Location)
                    .Select(f => f.Value).ToArray();

                if (affectedLocations.Length is 0)
                    return;

                var reportIds = (await _reportReadService.GetIdsByLocationsAsync(affectedLocations))
                    .Select(id => (ReportId) id);

                var reportsToUpdate = await _reportRepository.GetAsync(reportIds);

                foreach (var report in reportsToUpdate) report.DecrementPhoneNumberCount();

                await _reportRepository.UpdateRangeAsync(reportsToUpdate.ToArray());

                break;
            }
            case ContactFieldType.Email or ContactFieldType.Company:
            default:
                return;
        }
    }
}