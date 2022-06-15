using Services.Contacts.Application.DTO;
using Shared.Abstractions.Queries;

namespace Services.Contacts.Application.Queries;

public class GetContact : IQuery<ContactDto?>
{
    public GetContact(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}