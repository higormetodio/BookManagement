using BookManagement.Application.Models;
using BookManagement.Core.Interfaces;
using MediatR;

namespace BookManagement.Application.Commands.UpdateUserOnlyActive;
public class UpdateUserOnlyActiveHandler : IRequestHandler<UpdateUserOnlyActiveCommand, ResultViewModel>
{
    private readonly IUserRepository _repository;

    public UpdateUserOnlyActiveHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel> Handle(UpdateUserOnlyActiveCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetUserByIdAsync(request.Id);

        if (user is null)
        {
            return ResultViewModel.Error("User not found.");
        }

        user.ToActive(request.Active);

        await _repository.UpdateUserAsync(user);

        return ResultViewModel.Success();
    }
}
