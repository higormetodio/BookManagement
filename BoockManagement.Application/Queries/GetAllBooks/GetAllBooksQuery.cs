using BookManagement.Application.Models;
using MediatR;

namespace BookManagement.Application.Queries.GetAllBooks;
public class GetAllBooksQuery : IRequest<ResultViewModel<IEnumerable<BookviewModel>>>
{
    public GetAllBooksQuery(string query)
    {
        Query = query;
    }

    public string Query { get; private set; }
}
