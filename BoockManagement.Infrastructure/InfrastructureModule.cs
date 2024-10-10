using BookManagement.Core.Repositories;
using BookManagement.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BookManagement.Core.Services;
using BookManagement.Infrastructure.Auth;
using BookManagement.Infrastructure.Persistence.Repositories;

namespace BookManagement.Infrastructure;
public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<BookManagementDbContext>(options =>
                             options.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING", EnvironmentVariableTarget.User), b =>
                                                 b.MigrationsAssembly(typeof(BookManagementDbContext).Assembly.FullName)));

        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ILoanRepository, LoanRepository>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
