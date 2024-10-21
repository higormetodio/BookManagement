using BookManagement.Application.Email;
using BookManagement.Core.Enums;
using Microsoft.IdentityModel.Tokens;
using BookManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using BookManagement.Core.Repositories;
using BookManagement.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BookManagement.Application.BackgroundServices;
public class LateLoanNotificationService : BackgroundService
{
    private readonly IEmailService _emailService;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public LateLoanNotificationService(IEmailService emailService, IServiceScopeFactory serviceScopeFactory)
    {
        _emailService = emailService;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var today = DateTime.Today;

            using var scope = _serviceScopeFactory.CreateAsyncScope();
            var loanRepository = scope.ServiceProvider.GetRequiredService<ILoanRepository>();

            var loans = await loanRepository.GetAllLoansAsync();

            loans = loans.Where(l => l.Status != LoanStatus.Returned && l.ReturnDate.Date < today.Date).ToList();

            if (!loans.IsNullOrEmpty())
            {
                string subject = "BookLoanManagement - You have a late loan";

                foreach (var loan in loans)
                {
                    loan.LoanLate();
                    await loanRepository.UpdateLoanAsync(loan);

                    string message = $"Hi {loan.User.Name}, your loan for book {loan.Book.Title} is late and the book muste be returned. We are waiting. Thanks!";

                    await _emailService.SendEmailAsync(loan.User.Name, loan.User.Email, subject, message);
                }
            }

            await Task.Delay(TimeSpan.FromHours(24));
        }
    }
}
