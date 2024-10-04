using BookManagement.Application.Models;
using BookManagement.Core.Repositories;
using MediatR;

namespace BookManagement.Application.Queries.GetBookLoans;
public class GetBookLoansHandler : IRequestHandler<GetBookLoansQuery, ResultViewModel<BookLoansViewModel>>
{
    private readonly IBookRepository _repository;

    public GetBookLoansHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel<BookLoansViewModel>> Handle(GetBookLoansQuery request, CancellationToken cancellationToken)
    {
        var book = await _repository.GetBookLoansByIdAsync(request.Id);

        if (book is null || !book.Active)
        {
            return ResultViewModel<BookLoansViewModel>.Error("Book not found");
        }

        var loansWithUserViewModel = book.Loans.Select(LoanWithUserViewModel.FromEntity);
        var model = new BookLoansViewModel(book.Id, book.Title, book.Stock.Quantity, book.Stock.LoanQuantity, loansWithUserViewModel);

        return ResultViewModel<BookLoansViewModel>.Success(model);

    }
}
