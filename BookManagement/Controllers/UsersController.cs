using BookManagement.Application.Commands.DeleteUser;
using BookManagement.Application.Commands.CreateUser;
using BookManagement.Application.Commands.UpdateUser;
using BookManagement.Application.Commands.UpdateUserOnlyActive;
using BookManagement.Application.Queries.GetAllUsers;
using BookManagement.Application.Queries.GetUserById;
using BookManagement.Application.Queries.GetUserLoans;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string search = "")
    {
        var query = new GetAllUsersQuery(search);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetUserByIdQuery(id);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("{id:int}/loans")]
    public async Task<IActionResult> GetUserByIdLoans(int id)
    {
        var query = new GetUserLoansQuery(id);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateUserCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return CreatedAtAction(nameof(GetById), new { Id = result.Data }, command);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, UpdateUserCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Id from body does not match the given Id");
        }

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    [HttpPatch("{id:int}/UpdateUserActive")]
    public async Task<IActionResult> Patch(int id, JsonPatchDocument<UpdateUserOnlyActiveCommand> patchUser)
    {
        if (patchUser is null || id < 1)
        {
            return BadRequest("Invalid fields.");
        }

        var updateUserOnlyActive = new UpdateUserOnlyActiveCommand(id);

        patchUser.ApplyTo(updateUserOnlyActive);

        var result = await _mediator.Send(updateUserOnlyActive);

        return NoContent();

    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteUserCommand(id));

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }
}
