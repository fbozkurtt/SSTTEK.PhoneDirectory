using Services.Contacts.Domain.Aggregates.Contact;
using Services.Contacts.Domain.Factories;
using Shared.Abstractions.Commands;

namespace Services.Contacts.Application.Commands.Handler;

public class CreateContactHandler : ICommandHandler<CreateContact>
{
    private readonly IContactRepository _contactRepository;
    private readonly IContactFactory _contactFactory;

    public CreateContactHandler(IContactRepository contactRepository, IContactFactory contactFactory)
    {
        _contactRepository = contactRepository;
        _contactFactory = contactFactory;
    }

    public async Task HandleAsync(CreateContact command)
    {
        var (id, firstName, lastName) = command;

        var contact = _contactFactory.Create(id, firstName, lastName);

        await _contactRepository.AddAsync(contact);
    }
}