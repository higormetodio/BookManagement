using BookManagement.Application.Models;
using MediatR;

namespace BookManagement.Application.Commands.UpdateUserOnlyActive;
public class UpdateUserOnlyActiveCommand : IRequest<ResultViewModel>
{
    public UpdateUserOnlyActiveCommand(int id)
    {
        Id = id;
    }

    public int Id { get; private set; }
    public bool Active { get; set; }
}
