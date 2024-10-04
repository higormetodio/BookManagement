using BookManagement.Application.Models;
using BookManagement.Core.Interfaces;
using MediatR;

namespace BookManagement.Application.Commands.CreateUser;
public class CreateUserHandler : IRequestHandler<CreateUserCommand, ResultViewModel<int>>
{
    private readonly IUserRepository _repository;

    public CreateUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = request.ToEntity();

        await _repository.CreateUserAsync(user);

        return ResultViewModel<int>.Success(user.Id);
    }
}
