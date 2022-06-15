using Services.Contacts.Domain.Aggregates.Contact;
using Services.Contacts.Domain.Aggregates.Contact.ValueObjects;

namespace Services.Contacts.Domain.Factories;

public interface IContactFactory
{
    Contact Create(ContactId id, ContactFirstName firstName, ContactLastName? lastName);
}