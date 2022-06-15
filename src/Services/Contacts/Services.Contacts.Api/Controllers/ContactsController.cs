using Microsoft.AspNetCore.Mvc;
using Services.Contacts.Application.Commands;
using Services.Contacts.Application.DTO;
using Services.Contacts.Application.Queries;
using Shared.Abstractions.Commands;
using Shared.Abstractions.Queries;

namespace Services.Contacts.Api.Controllers;

public class ContactsController : BaseController
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public ContactsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ContactDto?>> Get(Guid id)
    {
        var result = await _queryDispatcher.QueryAsync(new GetContact(id));
        return OkOrNotFound(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContactDto>>> Get()
    {
        var result = await _queryDispatcher.QueryAsync(new GetContacts());
        return OkOrNotFound(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateContact command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return CreatedAtAction(nameof(Get), new {id = command.Id}, null);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] UpdateContact command)
    {
        await _commandDispatcher.DispatchAsync(command.SetId(id));
        return Ok();
    }

    [HttpPut("{contactId:guid}/fields")]
    public async Task<IActionResult> Put([FromBody] AddContactField command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return Ok();
    }

    [HttpDelete("{contactId:guid}/fields")]
    public async Task<IActionResult> Delete([FromBody] RemoveContactField command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromBody] DeleteContact command)
    {
        await _commandDispatcher.DispatchAsync(command);
        return Ok();
    }
}