using BookManagement.Application.Models;
using BookManagement.Core.Enums;
using BookManagement.Core.Repositories;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace BookManagement.Application.Queries.GetAllLoans;
public class GetAllLoansHandler : IRequestHandler<GetAllLoansQuery, ResultViewModel<IEnumerable<LoanViewModel>>>
{
    private readonly ILoanRepository _repository;

    public GetAllLoansHandler(ILoanRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel<IEnumerable<LoanViewModel>>> Handle(GetAllLoansQuery request, CancellationToken cancellationToken)
    {
        var loans = await _repository.GetAllLoansAsync();

        loans = loans.Where(l => l.Status != LoanStatus.Returned).ToList();

        if (!string.IsNullOrEmpty(request.Query))
        {
            loans = loans.Where(l => l.Book.Title.ToLower().Contains(request.Query.ToLower())).ToList();
        }

        if (loans.IsNullOrEmpty())
        {
            return ResultViewModel<IEnumerable<LoanViewModel>>.Error("Loans not found.");
        }

        var model = loans.Select(LoanViewModel.FromEntity);

        return ResultViewModel<IEnumerable<LoanViewModel>>.Success(model);
    }
}
