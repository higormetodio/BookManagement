using BookManagement.Application.Models;
using BookManagement.Core.Interfaces;
using MediatR;

namespace BookManagement.Application.Queries.GetAllBooks;
public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, ResultViewModel<IEnumerable<BookviewModel>>>
{
    private readonly IBookRepository _repository;

    public GetAllBooksHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel<IEnumerable<BookviewModel>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _repository.GetAllBooksAsync();

        books = books.Where(b => b.Active);

        if (!string.IsNullOrEmpty(request.Query))
        {
            books = books.Where(b => b.Title.ToLower().Contains(request.Query.ToLower()));
        }

        var model = books.Select(BookviewModel.FromEntity);

        return ResultViewModel<IEnumerable<BookviewModel>>.Success(model);
    }
}
