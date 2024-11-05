using BookManagement.Application.Models;
using BookManagement.Core.Repositories;
using MediatR;

namespace BookManagement.Application.Commands.UpdateBook;
public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, ResultViewModel>
{
    private readonly IBookRepository _repository;

    public UpdateBookHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _repository.GetBookByIdAsync(request.BookId);

        if (book is null || !book.Active)
        {
            return ResultViewModel.Error("Book not found");
        }

        book.Update(request.Title, request.Author, request.ISBN, request.PublicationYear);

        await _repository.UpdateBookAsync(book);

        return ResultViewModel.Success();
    }
}
