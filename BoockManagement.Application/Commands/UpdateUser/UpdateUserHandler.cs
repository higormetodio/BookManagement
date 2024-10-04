using BookManagement.Application.Models;
using BookManagement.Core.Repositories;
using MediatR;

namespace BookManagement.Application.Commands.UpdateUser;
public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, ResultViewModel>
{
    private readonly IUserRepository _repository;

    public UpdateUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetUserByIdAsync(request.Id);

        if (user is null)
        {
            return ResultViewModel.Error("User not found");
        }

        user.Update(request.Name, request.Email);

        await _repository.UpdateUserAsync(user);

        return ResultViewModel.Success();
    }
}
