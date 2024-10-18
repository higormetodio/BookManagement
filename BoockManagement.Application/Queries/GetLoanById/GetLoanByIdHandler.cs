using BookManagement.Application.Models;
using BookManagement.Core.Enums;
using BookManagement.Core.Repositories;
using MediatR;

namespace BookManagement.Application.Queries.GetLoanById;
public class GetLoanByIdHandler : IRequestHandler<GetLoanByIdQuery, ResultViewModel<LoanDetailViewModel>>
{
    private readonly ILoanRepository _repository;

    public GetLoanByIdHandler(ILoanRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel<LoanDetailViewModel>> Handle(GetLoanByIdQuery request, CancellationToken cancellationToken)
    {
        var loan = await _repository.GetLoanByIdAsync(request.Id);

        if (loan is null || loan.Status == LoanStatus.Returned)
        {
            return ResultViewModel<LoanDetailViewModel>.Error("Loan not found.");
        }

        var model = LoanDetailViewModel.FromEntity(loan);

        return ResultViewModel<LoanDetailViewModel>.Success(model);

    }
}
