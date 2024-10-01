using BookManagement.Core.Entities;

namespace BookManagement.Core.Interfaces;
public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(int id);
    Task<User> GetUserLoansByIdAsync(int id);
    Task<int> CreateUserAsync(User user);
    Task UpdateUserAsync(User user);
}
