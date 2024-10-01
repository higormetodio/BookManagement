using BookManagement.Application.Models;
using BookManagement.Core.Interfaces;
using MediatR;

namespace BookManagement.Application.Commands.InsertUser;
public class InsertUserHandler : IRequestHandler<InsertUserCommand, ResultViewModel<int>>
{
    private readonly IUserRepository _repository;

    public InsertUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel<int>> Handle(InsertUserCommand request, CancellationToken cancellationToken)
    {
        var user = request.ToEntity();

        await _repository.CreateUserAsync(user);

        return ResultViewModel<int>.Success(user.Id);
    }
}
