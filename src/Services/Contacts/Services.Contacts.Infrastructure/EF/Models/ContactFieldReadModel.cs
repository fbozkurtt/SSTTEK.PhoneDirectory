using Services.Contacts.Domain.Enums;

namespace Services.Contacts.Infrastructure.EF.Models;

public class ContactFieldReadModel
{
    public Guid  Id { get; set; }
    public string Value { get; set; } = null!;
    public ContactFieldType Type { get; set; }
    
    public ContactReadModel Contact { get; set; } = null!;
}