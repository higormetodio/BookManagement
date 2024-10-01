using BookManagement.Application.Models;
using MediatR;

namespace BookManagement.Application.Queries.GetLoanById;
public class GetLoanByIdQuery : IRequest<ResultViewModel<LoanDetailViewModel>>
{
    public GetLoanByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; private set; }
}
