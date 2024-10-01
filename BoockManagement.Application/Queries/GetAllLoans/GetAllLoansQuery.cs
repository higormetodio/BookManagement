using BookManagement.Application.Models;
using MediatR;

namespace BookManagement.Application.Queries.GetAllLoans;
public class GetAllLoansQuery : IRequest<ResultViewModel<IEnumerable<LoanViewModel>>>
{
    public GetAllLoansQuery(string query)
    {
        Query = query;
    }

    public string Query { get; private set; }
}
