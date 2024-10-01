using BookManagement.Application.Models;
using BookManagement.Core.Interfaces;
using MediatR;

namespace BookManagement.Application.Commands.InsertBook;
public class InserBookHandler : IRequestHandler<InsertBookCommand, ResultViewModel<int>>
{
    private readonly IBookRepository _repository;

    public InserBookHandler(IBookRepository repository)
    {
        _repository = repository;
    }       

    public async Task<ResultViewModel<int>> Handle(InsertBookCommand request, CancellationToken cancellationToken)
    {
        var book = request.ToEntity();

        await _repository.CreateBookAsync(book);

        return ResultViewModel<int>.Success(book.Id);
    }
}
