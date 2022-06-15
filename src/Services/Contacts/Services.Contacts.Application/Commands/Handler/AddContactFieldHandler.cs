using Services.Contacts.Application.Exceptions;
using Services.Contacts.Domain.Aggregates.Contact;
using Services.Contacts.Domain.Aggregates.Contact.ValueObjects;
using Shared.Abstractions.Commands;

namespace Services.Contacts.Application.Commands.Handler;

public class AddContactFieldHandler : ICommandHandler<AddContactField>
{
    private readonly IContactRepository _contactRepository;

    public AddContactFieldHandler(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task HandleAsync(AddContactField command)
    {
        var (contactId, value, type) = command;

        var contact = await _contactRepository.GetAsync(contactId);

        if (contact is null) throw new ContactNotFoundException(contactId);

        var field = new ContactField(value, type);
        contact.AddField(field);

        await _contactRepository.UpdateAsync(contact);
    }
}