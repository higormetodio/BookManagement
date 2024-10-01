using BookManagement.Application.Models;
using BookManagement.Core.Entities;
using MediatR;

namespace BookManagement.Application.Commands.UpdateBookStock;
public class UpdateBookStockCommand : IRequest<ResultViewModel<int>>
{
    public int BookId { get; set; }
    public int Quantity { get; set; }
}
