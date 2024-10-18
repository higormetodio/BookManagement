using BookManagement.Application.Models;
using BookManagement.Core.Repositories;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace BookManagement.Application.Queries.GetAllUsers;
public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, ResultViewModel<IEnumerable<UserViewModel>>>
{
    private readonly IUserRepository _repository;

    public GetAllUsersHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel<IEnumerable<UserViewModel>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _repository.GetAllUsersAsync();

        users = users.Where(u => u.Active).ToList();

        if (!request.Query.IsNullOrEmpty())
        {
            users = users.Where(u => u.Name.ToLower().Contains(request.Query.ToLower())).ToList();
        }

        if (users.IsNullOrEmpty())
        {
            return ResultViewModel<IEnumerable<UserViewModel>>.Error("Users with searched criteria, not found.");
        }

        var model = users.Select(UserViewModel.FromEntity);

        return ResultViewModel<IEnumerable<UserViewModel>>.Success(model);
    }
}
