using System;
using System.Threading.Tasks;
using NSubstitute;
using Services.Contacts.Application.Commands;
using Services.Contacts.Application.Commands.Handler;
using Services.Contacts.Domain.Aggregates.Contact;
using Services.Contacts.Domain.Exceptions;
using Services.Contacts.Domain.Factories;
using Shared.Abstractions.Commands;
using Shouldly;
using Xunit;

namespace Contacts.UnitTests.Application;

public class CreateContactHandlerTests
{
    private Task Act(CreateContact command)
        => _commandHandler.HandleAsync(command);

    [Fact]
    public async Task HandleAsync_Throws_EmptyContactFirstNameException_When_First_Name_Is_Empty()
    {
        var command = new CreateContact(Guid.NewGuid(), string.Empty, "Bozkurt");

        var exception = await Record.ExceptionAsync(() => Act(command));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EmptyContactFirstNameException>();
    }

    [Fact]
    public async Task HandleAsync_Calls_Repository_On_Success()
    {
        var command = new CreateContact(Guid.NewGuid(), "Furkan", "Bozkurt");

        _factory.Create(command.Id, command.FirstName, command.LastName).Returns(default(Contact));

        var exception = await Record.ExceptionAsync(() => Act(command));

        exception.ShouldBeNull();
        await _repository.Received(1).AddAsync(Arg.Any<Contact>());
    }

    #region ARRANGE

    private readonly ICommandHandler<CreateContact> _commandHandler;
    private readonly IContactRepository _repository;
    private readonly IContactFactory _factory;

    public CreateContactHandlerTests()
    {
        _repository = Substitute.For<IContactRepository>();
        _factory = Substitute.For<IContactFactory>();

        _commandHandler = new CreateContactHandler(_repository, _factory);
    }

    #endregion
}