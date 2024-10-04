using BookManagement.Application.Models;
using BookManagement.Core.Repositories;
using MediatR;

namespace BookManagement.Application.Queries.GetAllUsers;
public class GetUsersHandler : IRequestHandler<GetAllUsersQuery, ResultViewModel<IEnumerable<UserViewModel>>>
{
    private readonly IUserRepository _repository;

    public GetUsersHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel<IEnumerable<UserViewModel>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _repository.GetAllUsersAsync();

        users = users.Where(u => u.Active);

        if (!string.IsNullOrEmpty(request.Query))
        {
            users = users.Where(u => u.Name.ToLower().Contains(request.Query.ToLower()));
        }

        var model = users.Select(UserViewModel.FromEntity);

        return ResultViewModel<IEnumerable<UserViewModel>>.Success(model);
    }
}
