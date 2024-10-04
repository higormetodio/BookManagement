using BookManagement.Application.Commands.DeleteBook;
using BookManagement.Application.Commands.CreateBook;
using BookManagement.Application.Commands.UpdateBook;
using BookManagement.Application.Commands.UpdateBookOnlyActive;
using BookManagement.Application.Commands.UpdateBookStock;
using BookManagement.Application.Queries.GetAllBooks;
using BookManagement.Application.Queries.GetBookById;
using BookManagement.Application.Queries.GetBookLoans;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IMediator _mediator;

    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }    

    [HttpGet]
    public async Task<IActionResult> Get(string search = "")
    {
        var query = new GetAllBooksQuery(search);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetBookByIdQuery(id);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("{id:int}/loans")]
    public async Task<IActionResult> GetBookByIdLoans(int id)
    {
        var query = new GetBookLoansQuery(id);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> Post(CreateBookCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return CreatedAtAction(nameof(GetById), new { Id = result.Data }, command);
    }

    [HttpPut("{id:int}/stock")]
    public async Task<IActionResult> Put(int id, UpdateBookStockCommand command)
    {
        if (id != command.BookId)
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

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, UpdateBookCommand command)
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

    [HttpPatch("{id:int}/updateBookActive")]
    public async Task<IActionResult> Patch(int id, JsonPatchDocument<UpdateBookOnlyActiveCommand> patchBook)
    {
        if (patchBook is null || id < 1)
        {
            return BadRequest("Invalid fields.");
        }

        var updateBookOnlyActive = new UpdateBookOnlyActiveCommand(id);

        patchBook.ApplyTo(updateBookOnlyActive);

        var result = await _mediator.Send(updateBookOnlyActive);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteBookCommand(id));

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return NoContent();
    }
}
