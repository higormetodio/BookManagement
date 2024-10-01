using BookManagement.Application.Models;
using BookManagement.Core.Interfaces;
using MediatR;

namespace BookManagement.Application.Queries.GetUserById;
public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, ResultViewModel<UserViewModel>>
{
    private readonly IUserRepository _repository;

    public GetUserByIdHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel<UserViewModel>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetUserByIdAsync(request.Id);

        if (user is null || !user.Active)
        {
            return ResultViewModel<UserViewModel>.Error("User not found.");
        }

        var model = UserViewModel.FromEntity(user);

        return ResultViewModel<UserViewModel>.Success(model);
    }
}
