using BookManagement.Application.Commands.CreateLoan;
using BookManagement.Application.Commands.ReturnLoan;
using BookManagement.Application.Queries.GetAllLoans;
using BookManagement.Application.Queries.GetLoanById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "admin")]
[Produces("application/json")]
public class LoansController : ControllerBase
{
    private readonly IMediator _mediator;

    public LoansController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get a list of all Loans objects or some Loans objects by Book Title 
    /// </summary>
    /// <param name="search"></param>
    /// <returns>A list Loans objects or some Loans objects by Book Title</returns>
    [HttpGet]
    public async Task<IActionResult> Get(string search = "")
    {
        var query = new GetAllLoansQuery(search);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
        {
            return NotFound(result.Message);
        }

        return Ok(result);
    }

    /// <summary>
    /// Get a Loan object by Loan Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>A Loan objcet by Loan Id</returns>
    [HttpGet("{id:int:min(1)}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetLoanByIdQuery(id);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
        {
            return NotFound(result.Message);
        }

        return Ok(result);
    }

    /// <summary>
    /// Create a new Loan object
    /// </summary>
    /// <remarks>
    /// Request example
    /// 
    ///     POST api/version/loans
    ///     {
    ///         "userId": 4,
    ///         "bookId": 7
    ///
    ///     }
    /// </remarks>
    /// <param name="command"></param>
    /// <returns>A new Loan object created</returns>
    /// <remarks>Return a new Loan object created</remarks>
    [HttpPost]
    public async Task<IActionResult> Post(CreateLoanCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return CreatedAtAction(nameof(GetById), new { Id = result.Data }, command);
    }

    /// <summary>
    /// Update an existing Loan object with Loan Status as Returned
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Does not return contetnt</returns>
    [HttpPut("{id:int:min(1)}/returnLoan")]
    public async Task<IActionResult> Put(int id)
    {
        var command = new ReturnLoanCommand(id);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }
}
