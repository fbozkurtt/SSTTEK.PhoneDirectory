using Services.Reports.Domain.Entities.Report.ValueObjects;
using Services.Reports.Domain.Exceptions;

namespace Services.Reports.Domain.Entities.Report;

public class Report
{
    private ReportLocation _location = null!;
    private uint _numberOfContacts;
    private uint _numberOfPhoneNumbers;

    private Report()
    {
    }

    internal Report(ReportId id, ReportLocation location)
    {
        Id = id;
        _location = location;
        _numberOfContacts = 0;
        _numberOfPhoneNumbers = 0;
    }

    public ReportId Id { get; } = null!;

    public void IncrementContactCount()
    {
        _numberOfContacts++;
    }

    public void IncrementPhoneNumberCount(uint value = 1)
    {
        _numberOfPhoneNumbers += value;
    }

    public void DecrementContactCount()
    {
        if (_numberOfContacts is 0)
            throw new ContactCountLessThanZeroException();
        _numberOfContacts--;
    }

    public void DecrementPhoneNumberCount(uint value = 1)
    {
        if ((_numberOfPhoneNumbers is 0 && value > 0) || value > _numberOfPhoneNumbers)
            throw new PhoneNumberCountLessThanZeroException();
        _numberOfPhoneNumbers -= value;
    }
}