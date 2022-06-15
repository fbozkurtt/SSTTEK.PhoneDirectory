namespace Services.Contacts.Infrastructure.EF.Models;

public class ContactReadModel
{
    public Guid Id { get; set; }
    public int Version { get; set; }
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public ICollection<ContactFieldReadModel> Fields { get; set; } = null!;
}