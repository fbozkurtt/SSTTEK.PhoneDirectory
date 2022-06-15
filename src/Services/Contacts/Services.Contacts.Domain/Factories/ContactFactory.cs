using Services.Contacts.Domain.Aggregates.Contact;
using Services.Contacts.Domain.Aggregates.Contact.ValueObjects;

namespace Services.Contacts.Domain.Factories;

public sealed class ContactFactory : IContactFactory
{
    public Contact Create(ContactId id, ContactFirstName firstName, ContactLastName? lastName)
    {
        return new(id, firstName, lastName);
    }
}