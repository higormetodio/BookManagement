using BookManagement.Application.Models;
using MediatR;

namespace BookManagement.Application.Queries.GetAllUsers;
public class GetAllUsersQuery : IRequest<ResultViewModel<IEnumerable<UserViewModel>>>
{
    public GetAllUsersQuery(string query)
    {
        Query = query;
    }

    public string Query { get; private set; }
}
