using Services.Contacts.Application.Exceptions;
using Services.Contacts.Domain.Aggregates.Contact;
using Shared.Abstractions.Commands;

namespace Services.Contacts.Application.Commands.Handler;

public class UpdateContactHandler : ICommandHandler<UpdateContact>
{
    private readonly IContactRepository _contactRepository;

    public UpdateContactHandler(IContactRepository contactRepository)
        => _contactRepository = contactRepository;

    public async Task HandleAsync(UpdateContact command)
    {
        var contact = await _contactRepository.GetAsync(command.Id);

        if (contact is null)
        {
            throw new ContactNotFoundException(command.Id);
        }

        if (command.FirstName is not null)
            contact.SetFirstName(command.FirstName);

        contact.SetLastName(command.LastName);

        await _contactRepository.UpdateAsync(contact);
    }
}