using BookManagement.Application.Models;
using BookManagement.Core.Repositories;
using MediatR;

namespace BookManagement.Application.Commands.UpdateBookStock;
public class UpdateBookStockHandler : IRequestHandler<UpdateBookStockCommand, ResultViewModel>
{
    private readonly IBookRepository _repository;

    public UpdateBookStockHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel> Handle(UpdateBookStockCommand request, CancellationToken cancellationToken)
    {
        var book = await _repository.GetBookByIdAsync(request.BookId);

        if (book is null || !book.Active)
        {
            return ResultViewModel<int>.Error("Book not found.");
        }

        book.Stock.Update(request.Quantity);

        await _repository.UpdateBookStockAsync(book.Stock);

        return ResultViewModel.Success();
    }
}
