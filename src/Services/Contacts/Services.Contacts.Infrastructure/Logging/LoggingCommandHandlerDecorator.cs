using Microsoft.Extensions.Logging;
using Shared.Abstractions.Commands;

namespace Services.Contacts.Infrastructure.Logging;

internal sealed class LoggingCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    where TCommand : class, ICommand
{
    private readonly ICommandHandler<TCommand> _commandHandler;
    private readonly ILogger<LoggingCommandHandlerDecorator<TCommand>> _logger;

    public LoggingCommandHandlerDecorator(ICommandHandler<TCommand> commandHandler,
        ILogger<LoggingCommandHandlerDecorator<TCommand>> logger)
    {
        _commandHandler = commandHandler;
        _logger = logger;
    }

    public async Task HandleAsync(TCommand command)
    {
        var commandType = command.GetType().Name;

        try
        {
            _logger.LogInformation("The command '{CommandType}' requested", commandType);
            await _commandHandler.HandleAsync(command);
            _logger.LogInformation("The command '{CommandType}' handled successfully", commandType);
        }
        catch
        {
            _logger.LogError("An error occured while handling the command '{CommandType}'", commandType);
            throw;
        }
    }
}