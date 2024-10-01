using BookManagement.Application.Models;
using BookManagement.Core.Interfaces;
using MediatR;

namespace BookManagement.Application.Commands.UpdateBookOnlyActive;
public class UpdateBookOnlyActiveHandler : IRequestHandler<UpdateBookOnlyActiveCommand, ResultViewModel>
{
    private readonly IBookRepository _repository;

    public UpdateBookOnlyActiveHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel> Handle(UpdateBookOnlyActiveCommand request, CancellationToken cancellationToken)
    {
        var book = await _repository.GetBookByIdAsync(request.Id);

        if (book is null)
        {
            return ResultViewModel.Error("Book not found.");
        }

        book.ToActive(request.Active);
        await _repository.UpdateBookAsync(book);

        return ResultViewModel.Success();

    }
}
