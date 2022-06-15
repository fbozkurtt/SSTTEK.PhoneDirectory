using Services.Contacts.Domain.Enums;
using Services.Contacts.Domain.Exceptions;

namespace Services.Contacts.Domain.Aggregates.Contact.ValueObjects;

public record ContactField
{
    public ContactField(string value, ContactFieldType type)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new EmptyContactFieldException();

        switch (type)
        {
            case ContactFieldType.Email:
            {
                var splitValue = value.Split("@");
                if (splitValue.Length is not 2 || splitValue.Last().Split(".").Length is not 2)
                    throw new InvalidEmailFormatException(value);

                break;
            }
            case ContactFieldType.Phone:
                if (value.All(char.IsDigit) is false)
                    throw new InvalidPhoneNumberFormatException(value);
                break;
            case ContactFieldType.Location or ContactFieldType.Company:
                break;
            default:
                throw new InvalidContactFieldTypeException(type);
        }

        Value = value.ToUpperInvariant();
        Type = type;
    }

    public string Value { get; }
    public ContactFieldType Type { get; }
}