using BookManagement.Application.Models;
using MediatR;

namespace BookManagement.Application.Queries.GetBookById;
public class GetBookByIdQuery : IRequest<ResultViewModel<BookDetailViewModel>>
{
    public GetBookByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; private set; }
}
