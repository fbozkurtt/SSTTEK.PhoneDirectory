using Microsoft.EntityFrameworkCore;
using Services.Contacts.Application.DTO;
using Services.Contacts.Application.Queries;
using Services.Contacts.Infrastructure.EF.Contexts;
using Services.Contacts.Infrastructure.EF.Models;
using Shared.Abstractions.Queries;

namespace Services.Contacts.Infrastructure.EF.Queries.Handlers;

internal sealed class GetContactHandler : IQueryHandler<GetContact, ContactDto?>
{
    private readonly DbSet<ContactReadModel> _contacts;

    public GetContactHandler(ReadDbContext dbContext)
        => _contacts = dbContext.Contacts;

    public Task<ContactDto?> HandleAsync(GetContact query)
        => _contacts
            .Include(c => c.Fields)
            .Where(c => c.Id == query.Id)
            .Select(c => c.AsDto())
            .AsNoTracking()
            .SingleOrDefaultAsync();
}