using Microsoft.EntityFrameworkCore;
using Services.Contacts.Domain.Aggregates.Contact;
using Services.Contacts.Domain.Aggregates.Contact.Events;
using Services.Contacts.Domain.Aggregates.Contact.ValueObjects;
using Services.Contacts.Infrastructure.EF.Contexts;
using Shared.Abstractions.Events;

namespace Services.Contacts.Infrastructure.EF.Repositories;

internal sealed class PostgresContactRepository : IContactRepository
{
    private readonly DbSet<Contact> _contacts;
    private readonly IDomainEventPublisher _eventPublisher;
    private readonly WriteDbContext _writeDbContext;

    public PostgresContactRepository(WriteDbContext writeDbContext, IDomainEventPublisher eventPublisher)
    {
        _contacts = writeDbContext.Contacts;
        _writeDbContext = writeDbContext;
        _eventPublisher = eventPublisher;
    }

    public Task<Contact?> GetAsync(ContactId id)
    {
        return _contacts.Include("_fields").SingleOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddAsync(Contact contact)
    {
        await _contacts.AddAsync(contact);
        await _eventPublisher.PublishAsync(contact.Events.ToArray());
        await _writeDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Contact contact)
    {
        _contacts.Update(contact);
        await _eventPublisher.PublishAsync(contact.Events.ToArray());
        await _writeDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Contact contact)
    {
        _contacts.Remove(contact);
        await _eventPublisher.PublishAsync(new ContactDeleted(contact.GetFields()));
        await _writeDbContext.SaveChangesAsync();
    }
}