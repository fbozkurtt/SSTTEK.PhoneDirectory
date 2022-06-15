using MassTransit;
using Services.Contacts.Domain.Aggregates.Contact.Events;
using Services.Contacts.Domain.Enums;
using Services.Reports.Application.Exceptions;
using Services.Reports.Application.Services;
using Services.Reports.Domain.Entities.Report;
using Services.Reports.Domain.Entities.Report.ValueObjects;
using Services.Reports.Domain.Factories;

namespace Services.Reports.Application.Events.Consumers;

public class ContactFieldAddedConsumer : IConsumer<ContactFieldAdded>
{
    private readonly IReportRepository _reportRepository;
    private readonly IReportReadService _reportReadService;
    private readonly IReportFactory _reportFactory;

    public ContactFieldAddedConsumer(IReportRepository reportRepository,
        IReportReadService reportReadService,
        IReportFactory reportFactory)
    {
        _reportRepository = reportRepository;
        _reportReadService = reportReadService;
        _reportFactory = reportFactory;
    }

    public async Task Consume(ConsumeContext<ContactFieldAdded> context)
    {
        var field = context.Message.Field;

        switch (field.Type)
        {
            case ContactFieldType.Location:
            {
                var location = field.Value;

                var reportId = await _reportReadService.GetIdByLocationAsync(location);

                if (reportId is null)
                {
                    var report = _reportFactory.Create(Guid.NewGuid(), location);
                    var phoneNumberCount = context.Message.Fields.Count(f => f.Type is ContactFieldType.Phone);

                    report.IncrementContactCount();
                    report.IncrementPhoneNumberCount((uint) phoneNumberCount);
                    await _reportRepository.AddAsync(report);
                }
                else
                {
                    var report = await _reportRepository.GetAsync(reportId);
                    if (report is null)
                        throw new ReportNotFoundException(reportId.GetValueOrDefault());

                    var phoneNumberCount = context.Message.Fields.Count(f => f.Type is ContactFieldType.Phone);
                    report.IncrementContactCount();
                    report.IncrementPhoneNumberCount((uint) phoneNumberCount);

                    await _reportRepository.UpdateAsync(report);
                }

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

                foreach (var report in reportsToUpdate)
                {
                    report.IncrementPhoneNumberCount();
                }

                await _reportRepository.UpdateRangeAsync(reportsToUpdate.ToArray());

                break;
            }
            case ContactFieldType.Email or ContactFieldType.Company:
            default:
                return;
        }
    }
}