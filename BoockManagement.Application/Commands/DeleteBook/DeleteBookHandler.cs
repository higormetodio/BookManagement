using BookManagement.Application.Models;
using BookManagement.Core.Enums;
using BookManagement.Core.Repositories;
using MediatR;

namespace BookManagement.Application.Commands.DeleteBook;
public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, ResultViewModel>
{
    private readonly IBookRepository _repository;

    public DeleteBookHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _repository.GetBookLoansByIdAsync(request.Id);

        if (book is null || !book.Active)
        {
            return ResultViewModel.Error("Book not found.");
        }

        var isLoaned = book.Loans.Any(l => l.Status != LoanStatus.Returned);

        if (isLoaned)
        {
            return ResultViewModel.Error("The book cannot be deleted as it has active loans.");
        }

        book.ToActive(false);
        await _repository.UpdateBookAsync(book);

        return ResultViewModel.Success();
    }
}
