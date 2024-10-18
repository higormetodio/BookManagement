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
using BookManagement.Application.Commands.LoginUser;
using Microsoft.AspNetCore.Authorization;

namespace BookManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Get(string name = "")
    {
        var query = new GetAllUsersQuery(name);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
        {
            return NotFound(result.Message);
        }

        return Ok(result);
    }

    [HttpGet("{id:int:min(1)}")]
    [Authorize(Roles = "admin, user")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetUserByIdQuery(id);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
        {
            return NotFound(result.Message);
        }

        var loginEmail = User.FindFirst("userName")!.Value;
        var isAdmin = User.IsInRole("admin");

        if (loginEmail == result.Data.Email || isAdmin)
        {
            return Ok(result);
        }

        return Unauthorized(new { Status = "Erro", Message = "Unauthorized user. User does not have permission to search other users." });  
    }

    [HttpGet("{id:int:min(1)}/loans")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetUserByIdLoans(int id)
    {
        var query = new GetUserLoansQuery(id);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
        {
            return NotFound(result.Message);
        }

        var loginEmail = User.FindFirst("userName")!.Value;
        var isAdmin = User.IsInRole("admin");

        if (loginEmail == result.Data.Email || isAdmin)
        {
            return Ok(result);
        }

        return Unauthorized(new { Status = "Erro", Message = "Unauthorized user. User does not have permission to search other users." });
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post(CreateUserCommand command)
    {
        var isAdmin = User.IsInRole("admin");

        if (isAdmin)
        {
            command.Role = "admin";
        }

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { Id = result.Data }, command);
    }

    [HttpPut]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Put(UpdateUserCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    [HttpPatch("{id:int:min(1)}/UpdateUserActive")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Patch(int id, JsonPatchDocument<UpdateUserOnlyActiveCommand> patchUser)
    {
        if (patchUser is null || id < 1)
        {
            return BadRequest("Invalid fields.");
        }

        var updateUserOnlyActive = new UpdateUserOnlyActiveCommand(id);

        patchUser.ApplyTo(updateUserOnlyActive);

        var result = await _mediator.Send(updateUserOnlyActive);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();

    }

    [HttpDelete("{id:int:min(1)}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteUserCommand(id));

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    [HttpPut("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginUserCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return Ok(result);
    }
}
