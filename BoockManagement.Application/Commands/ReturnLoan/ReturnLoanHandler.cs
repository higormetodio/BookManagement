using BookManagement.Application.Models;
using BookManagement.Core.Enums;
using BookManagement.Core.Repositories;
using MediatR;

namespace BookManagement.Application.Commands.ReturnLoan;
public class ReturnLoanHandler : IRequestHandler<ReturnLoanCommand, ResultViewModel>
{
    private readonly ILoanRepository _repositoryLoan;
    private readonly IBookRepository _repositoryBook;

    public ReturnLoanHandler(ILoanRepository repository, IBookRepository repositoryBook)
    {
        _repositoryLoan = repository;
        _repositoryBook = repositoryBook;
    }

    public async Task<ResultViewModel> Handle(ReturnLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = await _repositoryLoan.GetLoanByIdAsync(request.Id);

        if (loan is null || loan.Status == LoanStatus.Returned)
        {
            return ResultViewModel.Error("Loan not found");
        }

        var book = await _repositoryBook.GetBookByIdAsync(loan.BookId);

        loan.LoanReturned();
        await _repositoryLoan.UpdateLoanAsync(loan);

        book.Stock.ReturnStockMovement();
        await _repositoryBook.UpdateBookStockAsync(book.Stock);

        return ResultViewModel.Success();
    }
}
