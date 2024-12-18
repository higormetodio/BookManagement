﻿using BookManagement.Application.Commands.DeleteBook;
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
using Microsoft.AspNetCore.Authorization;

namespace BookManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "admin")]
[Produces("application/json")]
public class BooksController : ControllerBase
{
    private readonly IMediator _mediator;

    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get a list of all Book objects or some Book objects by Book Title 
    /// </summary>
    /// <param name="title"></param>
    /// <returns>A list Books objects or some Books objects by Book Title</returns>
    [HttpGet]
    public async Task<IActionResult> Get(string title = "")
    {
        var query = new GetAllBooksQuery(title);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Get a Book object by Book Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>A Book objcet by Book Id</returns>
    [HttpGet("{id:int:min(1)}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetBookByIdQuery(id);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Get a Book object by Book Id with all Loans
    /// </summary>
    /// <param name="id"></param>
    /// <returns>A Book object by Book Id with all Loans</returns>
    [HttpGet("loans/{id:int:min(1)}")]
    public async Task<IActionResult> GetBookByIdLoans(int id)
    {
        var query = new GetBookLoansQuery(id);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Create a new Book object
    /// </summary>
    /// <remarks>
    /// Request example
    /// 
    ///     POST api/version/books
    ///     {
    ///         "title": "Sistemas distribuídos: princípios e paradigmas",
    ///         "author": "Andrew S. Tanenbaum",
    ///         "isbn": "B00VQGOWH4",
    ///         "publicationYear": 2015
    ///     }
    /// </remarks>
    /// <param name="command"></param>
    /// <returns>A new Book object created</returns>
    /// <remarks>Return a new Book object created</remarks>
    [HttpPost]
    public async Task<IActionResult> Post(CreateBookCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return CreatedAtAction(nameof(GetById), new { Id = result.Data }, result);
    }

    /// <summary>
    /// Update an existing BookStock object
    /// </summary>
    /// <param name="command"></param>
    /// <returns>Does not return contetnt</returns>
    [HttpPut("stock")]
    public async Task<IActionResult> Put(UpdateBookStockCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Update an existing Book object
    /// </summary>
    /// <param name="command"></param>
    /// <returns>Does not return contetnt</returns>
    [HttpPut]
    public async Task<IActionResult> Put(UpdateBookCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Update only the status Active of an existing Book object
    /// </summary>
    /// <remarks>
    /// Request example
    /// 
    ///     PATCH api/version/books/updateBookActive/id
    ///     [
    ///         {
    ///             "path": "/Active",
    ///             "op": "replace",
    ///             "value": "true"
    ///         }
    ///     ]
    /// </remarks>
    /// <param name="id"></param>
    /// <param name="patchBook"></param>
    /// <returns>Does not return contetnt</returns>
    [HttpPatch("updateBookActive/{id:int:min(1)}")]
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
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Update an existing Book object with status Active = false
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Does not return contetnt</returns>
    [HttpDelete("{id:int:min(1)}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteBookCommand(id));

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}
