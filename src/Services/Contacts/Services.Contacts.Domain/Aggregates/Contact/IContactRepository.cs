using Services.Contacts.Domain.Aggregates.Contact.ValueObjects;

namespace Services.Contacts.Domain.Aggregates.Contact;

public interface IContactRepository
{
    Task<Contact?> GetAsync(ContactId id);
    Task AddAsync(Contact contact);
    Task UpdateAsync(Contact contact);
    Task DeleteAsync(Contact contact);
}