namespace Services.Contacts.Application.DTO;

public class ContactDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public IEnumerable<ContactFieldDto> Fields { get; set; } = null!;
}