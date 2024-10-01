using BookManagement.Application.Models;
using BookManagement.Core.Interfaces;
using MediatR;

namespace BookManagement.Application.Commands.UpdateBookStock;
public class UpdateBookStockHandler : IRequestHandler<UpdateBookStockCommand, ResultViewModel<int>>
{
    private readonly IBookRepository _repository;

    public UpdateBookStockHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel<int>> Handle(UpdateBookStockCommand request, CancellationToken cancellationToken)
    {
        var book = await _repository.GetBookByIdAsync(request.BookId);

        if (book is null)
        {
            return ResultViewModel<int>.Error("Book does not exist.");
        }

        book.Stock.Update(request.Quantity);

        _repository.UpdateBookStockAsync(book.Stock);

        return ResultViewModel<int>.Success(book.Id);
    }
}
