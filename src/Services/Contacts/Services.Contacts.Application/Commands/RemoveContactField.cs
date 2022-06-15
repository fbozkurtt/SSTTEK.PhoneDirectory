using Services.Contacts.Domain.Enums;
using Shared.Abstractions.Commands;

namespace Services.Contacts.Application.Commands;

public record RemoveContactField(Guid ContactId, string Value, ContactFieldType Type) : ICommand;