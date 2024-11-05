using BookManagement.Application.Models;
using BookManagement.Core.Repositories;
using BookManagement.Core.Services;
using MediatR;

namespace BookManagement.Application.Commands.UpdateUser;
public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, ResultViewModel>
{
    private readonly IUserRepository _repository;
    private readonly IAuthService _authService;

    public UpdateUserHandler(IUserRepository repository, IAuthService authService)
    {
        _repository = repository;
        _authService = authService;
    }

    public async Task<ResultViewModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetUserByIdAsync(request.UserId);

        if (user is null || !user.Active)
        {
            return ResultViewModel.Error("User not found");
        }

        var password = _authService.ComputeSha256Hash(request.Password);

        user.Update(request.Name, request.Email, request.BirthDate, password);

        await _repository.UpdateUserAsync(user);

        return ResultViewModel.Success();
    }
}
