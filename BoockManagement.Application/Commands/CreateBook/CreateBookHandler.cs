using BookManagement.Application.Models;
using BookManagement.Core.Interfaces;
using MediatR;

namespace BookManagement.Application.Commands.CreateBook;
public class CreateBookHandler : IRequestHandler<CreateBookCommand, ResultViewModel<int>>
{
    private readonly IBookRepository _repository;

    public CreateBookHandler(IBookRepository repository)
    {
        _repository = repository;
    }       

    public async Task<ResultViewModel<int>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = request.ToEntity();

        await _repository.CreateBookAsync(book);

        return ResultViewModel<int>.Success(book.Id);
    }
}
