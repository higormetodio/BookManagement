using BookManagement.Application.Models;
using BookManagement.Core.Enums;
using BookManagement.Core.Repositories;
using MediatR;

namespace BookManagement.Application.Commands.DeleteUser;
public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, ResultViewModel>
{
    private readonly IUserRepository _repository;

    public DeleteUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultViewModel> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetUserLoansByIdAsync(request.Id);

        if (user is null || !user.Active)
        {
            return ResultViewModel.Error("User not found.");
        }

        var isLoaned = user.Loans.Any(l => l.Status != LoanStatus.Returned);

        if (isLoaned)
        {
            return ResultViewModel.Error("The user cannot be deleted as he has active loans.");
        }

        user.ToActive(false);

        await _repository.UpdateUserAsync(user);

        return ResultViewModel.Success();
    }
}
