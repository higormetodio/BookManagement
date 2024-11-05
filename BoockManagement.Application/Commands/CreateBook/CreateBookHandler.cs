using BookManagement.Application.Models;
using BookManagement.Core.Entities;
using BookManagement.Core.Repositories;
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
        var book = await _repository.GetBookByIsbnAsync(request.ISBN);
        
        if (book != null)
        {
            return ResultViewModel<int>.Error("Book with ISBN already created");
        }
        
        book = request.ToEntity();

        await _repository.CreateBookAsync(book);

        return ResultViewModel<int>.Success(book.Id);
    }
}
