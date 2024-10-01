using BookManagement.Application.Models;
using BookManagement.Core.Interfaces;
using MediatR;

namespace BookManagement.Application.Queries.GetBookById;
public class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, ResultViewModel<BookDetailViewModel>>
{
    public GetBookByIdHandler(IBookRepository repository)
    {
        _repository = repository;
    }
    
    private readonly IBookRepository _repository;

    public async Task<ResultViewModel<BookDetailViewModel>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _repository.GetBookByIdAsync(request.Id);

        if (book is null || !book.Active)
        {
            return ResultViewModel<BookDetailViewModel>.Error("Book not found");
        }

        var model = BookDetailViewModel.FromEntity(book);

        return ResultViewModel<BookDetailViewModel>.Success(model);

    }
}
