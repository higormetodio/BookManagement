using BookManagement.Application.Models;
using BookManagement.Core.Repositories;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace BookManagement.Application.Queries.GetAllBooks;
public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, ResultViewModel<IEnumerable<BookViewModel>>>
{
    private readonly IBookRepository _repository;

    public GetAllBooksHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel<IEnumerable<BookViewModel>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _repository.GetAllBooksAsync();

        if (!request.Query.IsNullOrEmpty())
        {
            books = books.Where(b => b.Title.ToLower().Contains(request.Query.ToLower())).ToList();
        }

        if (books.IsNullOrEmpty())
        {
            return ResultViewModel<IEnumerable<BookViewModel>>.Error("Books with searched criteria, not found.");
        }

        var model = books.Select(BookViewModel.FromEntity);

        return ResultViewModel<IEnumerable<BookViewModel>>.Success(model);
    }
}
