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
[Produces("application/json")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get a list of all User objects or some User objects by User Name 
    /// </summary>
    /// <param name="name"></param>
    /// <returns>A list Users objects or some Users objects by User Name</returns>
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

    /// <summary>
    /// Get a User object by User Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>A User objcet by User Id</returns>
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

    /// <summary>
    /// Get a User object by User Id with all Loans
    /// </summary>
    /// <param name="id"></param>
    /// <returns>A User object by User Id with all Loans</returns>
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

    /// <summary>
    /// Create a new User object
    /// </summary>
    /// <remarks>
    /// Request example
    /// 
    ///     POST api/version/users
    ///     {
    ///         "name": "Raphael Pedreira",
    ///         "email": "raphael@gmail.com",
    ///         "birthDate": "1987-7-3",
    ///         "password": "12@34#56Ab"
    ///     }
    /// </remarks>
    /// <param name="command"></param>
    /// <returns>A new Book object created</returns>
    /// <remarks>Return a new Book object created</remarks>
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

    /// <summary>
    /// Update an existing User object
    /// </summary>
    /// <param name="command"></param>
    /// <returns>Does not return contetnt</returns>
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

    /// <summary>
    /// Update only the status Active of an existing User object
    /// </summary>
    /// <remarks>
    /// Request example
    /// 
    ///     PATCH api/version/users/id/updateUserActive
    ///     [
    ///         {
    ///             "path": "/Active",
    ///             "op": "replace",
    ///             "value": "true"
    ///         }
    ///     ]
    /// </remarks>
    /// <param name="id"></param>
    /// <param name="patchUser"></param>
    /// <returns>Does not return contetnt</returns>
    [HttpPatch("{id:int:min(1)}/updateUserActive")]
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

    /// <summary>
    /// Update an existing User object with status Active = false
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Does not return contetnt</returns>
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

    /// <summary>
    /// Login an existing User object
    /// </summary>
    /// <param name="command"></param>
    /// <returns>A login user with email and token</returns>
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
