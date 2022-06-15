using Services.Contacts.Application.Exceptions;
using Services.Contacts.Domain.Aggregates.Contact;
using Shared.Abstractions.Commands;

namespace Services.Contacts.Application.Commands.Handler;

public class DeleteContactHandler : ICommandHandler<DeleteContact>
{
    private readonly IContactRepository _contactRepository;

    public DeleteContactHandler(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task HandleAsync(DeleteContact command)
    {
        var contact = await _contactRepository.GetAsync(command.Id);

        if (contact is null) throw new ContactNotFoundException(command.Id);

        await _contactRepository.DeleteAsync(contact);
    }
}