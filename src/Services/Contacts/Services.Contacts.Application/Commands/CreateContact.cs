using Shared.Abstractions.Commands;

namespace Services.Contacts.Application.Commands;

public record CreateContact(Guid Id, string FirstName, string? LastName) : ICommand;