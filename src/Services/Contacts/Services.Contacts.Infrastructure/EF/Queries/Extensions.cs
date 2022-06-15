using Services.Contacts.Application.DTO;
using Services.Contacts.Infrastructure.EF.Models;

namespace Services.Contacts.Infrastructure.EF.Queries;

internal static class Extensions
{
    public static ContactDto AsDto(this ContactReadModel readModel)
        => new()
        {
            Id = readModel.Id,
            FirstName = readModel.FirstName,
            LastName = readModel.LastName,
            Fields = readModel.Fields.Select(cf => new ContactFieldDto()
            {
                Value = cf.Value,
                Type = cf.Type.ToString(),
            })
        };
}