using Services.Contacts.Application.Exceptions;
using Services.Contacts.Domain.Aggregates.Contact;
using Shared.Abstractions.Commands;

namespace Services.Contacts.Application.Commands.Handler;

public class RemoveContactFieldHandler : ICommandHandler<RemoveContactField>
{
    private readonly IContactRepository _contactRepository;

    public RemoveContactFieldHandler(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task HandleAsync(RemoveContactField command)
    {
        var (contactId, value, type) = command;
        var contact = await _contactRepository.GetAsync(contactId);

        if (contact is null)
        {
            throw new ContactNotFoundException(contactId);
        }

        contact.RemoveField(value, type);

        await _contactRepository.UpdateAsync(contact);
    }
}