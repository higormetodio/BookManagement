using BookManagement.Core.Entities;

namespace BookManagement.Core.Repositories;
public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(int id);
    Task<User> GetUserLoansByIdAsync(int id);
    Task<User> GetUserByEmailAndPasswordAsync(string email, string password);
    Task<int> CreateUserAsync(User user);
    Task UpdateUserAsync(User user);
}
