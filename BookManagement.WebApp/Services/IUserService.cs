using BookManagement.Application.Commands.CreateUser;
using BookManagement.Application.Commands.LoginUser;
using BookManagement.Application.Commands.UpdateUser;
using BookManagement.Application.Commands.UpdateUserOnlyActive;
using BookManagement.Application.Models;

namespace BookManagement.WebApp.Services;

public interface IUserService
{
    Task<ResultViewModel<IEnumerable<UserViewModel>>> GetAllUsers();
    Task<ResultViewModel<UserViewModel>> GetUserById(int id);
    Task<ResultViewModel<UserLoansViewModel>> GetUserByIdLoans(int id);
    Task<ResultViewModel<int>> CreateUser(CreateUserCommand command);
    Task<ResultViewModel> UpdateUser(UpdateUserCommand command);
    Task<ResultViewModel> UpdateUserOnlyActive(int id, UpdateUserOnlyActiveCommand command);
    Task<ResultViewModel> DeleteUser(int id);
    Task<ResultViewModel<LoginUserViewModel>> LoginUser(LoginUserCommand command);
}
