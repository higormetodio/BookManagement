using BookManagement.Application.Models;
using MediatR;

namespace BookManagement.Application.Commands.UpdateBookOnlyActive;
public class UpdateBookOnlyActiveCommand : IRequest<ResultViewModel>
{
    public UpdateBookOnlyActiveCommand(int id)
    {
        Id = id;
    }

    public int Id { get; private set; }
    public bool Active { get; set; }
}
