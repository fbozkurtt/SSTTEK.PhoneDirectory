using Microsoft.EntityFrameworkCore;
using Services.Contacts.Application.DTO;
using Services.Contacts.Application.Queries;
using Services.Contacts.Infrastructure.EF.Contexts;
using Services.Contacts.Infrastructure.EF.Models;
using Shared.Abstractions.Queries;

namespace Services.Contacts.Infrastructure.EF.Queries.Handlers;

internal sealed class GetContactsHandler : IQueryHandler<GetContacts, IEnumerable<ContactDto>>
{
    private readonly DbSet<ContactReadModel> _contacts;

    public GetContactsHandler(ReadDbContext dbContext)
    {
        _contacts = dbContext.Contacts;
    }

    public async Task<IEnumerable<ContactDto>> HandleAsync(GetContacts query)
    {
        return await _contacts
            .Include(c => c.Fields)
            .Select(c => c.AsDto())
            .AsNoTracking()
            .ToListAsync();
    }
}