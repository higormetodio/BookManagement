using BookManagement.Application.Models;
using BookManagement.Core.Repositories;
using MediatR;

namespace BookManagement.Application.Commands.CreateLoan;
public class CreateLoanHandler : IRequestHandler<CreateLoanCommand, ResultViewModel<int>>
{
    private readonly ILoanRepository _repositoryLoan;
    private readonly IBookRepository _repositoryBook;

    public CreateLoanHandler(ILoanRepository repository, IBookRepository repositoryBook)
    {
        _repositoryLoan = repository;
        _repositoryBook = repositoryBook;
    }

    public async Task<ResultViewModel<int>> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = request.ToEntity();

        var book = await _repositoryBook.GetBookByIdAsync(loan.BookId);

        if (book.Stock.Quantity <= 0)
        {
            return ResultViewModel<int>.Error("There is no book available.");
        }

        await _repositoryLoan.CreateLoanAsync(loan);        
        
        book.Stock.LoanStockMovement();
        await _repositoryBook.UpdateBookStockAsync(book.Stock);

        return ResultViewModel<int>.Success(loan.Id);
    }
}
