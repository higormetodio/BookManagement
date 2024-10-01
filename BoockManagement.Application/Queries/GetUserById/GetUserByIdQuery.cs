using BookManagement.Application.Models;
using MediatR;

namespace BookManagement.Application.Queries.GetUserById;
public class GetUserByIdQuery : IRequest<ResultViewModel<UserViewModel>>
{
    public GetUserByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; private set; }
}
