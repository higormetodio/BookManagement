using BookManagement.Application.Models;
using MediatR;

namespace BookManagement.Application.Commands.DeleteBook;
public class DeleteBookCommand : IRequest<ResultViewModel>
{
    public DeleteBookCommand(int id)
    {
        Id = id;
    }

    public int Id { get; private set; }
}
