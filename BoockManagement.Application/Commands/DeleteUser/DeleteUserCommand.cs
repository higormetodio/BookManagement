using BookManagement.Application.Models;
using MediatR;

namespace BookManagement.Application.Commands.DeleteUser;
public class DeleteUserCommand : IRequest<ResultViewModel>
{
    public DeleteUserCommand(int id)
    {
        Id = id;
    }
    public int Id { get; private set; }
}
