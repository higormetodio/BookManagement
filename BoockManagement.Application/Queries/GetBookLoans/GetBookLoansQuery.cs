using BookManagement.Application.Models;
using MediatR;

namespace BookManagement.Application.Queries.GetBookLoans;
public class GetBookLoansQuery : IRequest<ResultViewModel<BookLoansViewModel>>
{
    public GetBookLoansQuery(int id)
    {
        Id = id;
    }

    public int Id { get; private set; }
}
