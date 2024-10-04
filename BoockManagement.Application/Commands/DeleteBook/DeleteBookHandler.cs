using BookManagement.Application.Models;
using BookManagement.Core.Interfaces;
using MediatR;

namespace BookManagement.Application.Commands.DeleteBook;
public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, ResultViewModel>
{
    private readonly IBookRepository _repository;

    public DeleteBookHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _repository.GetBookByIdAsync(request.Id);

        if (book is null || !book.Active)
        {
            return ResultViewModel.Error("Book not found.");
        }

        book.ToActive(false);
        _repository.UpdateBookAsync(book);

        return ResultViewModel.Success();
    }
}
