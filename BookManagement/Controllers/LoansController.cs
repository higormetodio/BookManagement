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
public class LoansController : ControllerBase
{
    private readonly IMediator _mediator;

    public LoansController(IMediator mediator)
    {
        _mediator = mediator;
    }

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
