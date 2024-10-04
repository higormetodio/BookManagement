using BookManagement.Core.Entities;
using BookManagement.Core.Repositories;
using BookManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Infrastructure.Repositories;
public class UserRepository : IUserRepository
{
    private readonly BookManagementDbContext _context;

    public UserRepository(BookManagementDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
        => await _context.Users.AsNoTracking().ToListAsync();

    public async Task<User> GetUserByIdAsync(int id)
        => await _context.Users.FindAsync(id);

    public async Task<User> GetUserLoansByIdAsync(int id)
        => await _context.Users.Include(u => u.Loans)
                               .ThenInclude(l => l.Book)
                               .SingleOrDefaultAsync(u => u.Id == id);

    public async Task<int> CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user.Id;

    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}
