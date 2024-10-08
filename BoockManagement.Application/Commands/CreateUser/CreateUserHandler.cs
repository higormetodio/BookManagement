using BookManagement.Application.Models;
using BookManagement.Core.Repositories;
using BookManagement.Core.Services;
using MediatR;

namespace BookManagement.Application.Commands.CreateUser;
public class CreateUserHandler : IRequestHandler<CreateUserCommand, ResultViewModel<int>>
{
    private readonly IUserRepository _repository;
    private readonly IAuthService _authService;

    public CreateUserHandler(IUserRepository repository, IAuthService authService)
    {
        _repository = repository;
        _authService = authService;
    }

    public async Task<ResultViewModel<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var password = _authService.ComputeSha256Hash(request.Password);

        var user = request.ToEntity(password);

        await _repository.CreateUserAsync(user);

        return ResultViewModel<int>.Success(user.Id);
    }
}
