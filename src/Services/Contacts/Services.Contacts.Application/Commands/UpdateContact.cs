using Shared.Abstractions.Commands;

namespace Services.Contacts.Application.Commands;

public record UpdateContact: ICommand
{
    internal Guid Id { get; private set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public UpdateContact SetId(Guid id)
    {
        Id = id;
        return this;
    }
}