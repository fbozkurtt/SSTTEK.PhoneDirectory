using Shared.Abstractions.Commands;

namespace Services.Contacts.Application.Commands;

public record DeleteContact(Guid Id) : ICommand;