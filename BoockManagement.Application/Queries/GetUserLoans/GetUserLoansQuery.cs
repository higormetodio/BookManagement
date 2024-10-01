using BookManagement.Application.Models;
using MediatR;

namespace BookManagement.Application.Queries.GetUserLoans;
public class GetUserLoansQuery : IRequest<ResultViewModel<UserLoansViewModel>>
{
    public GetUserLoansQuery(int id)
    {
        Id = id;
    }

    public int Id { get; private set; }
}
