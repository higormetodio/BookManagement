using BookManagement.Application.Email;
using BookManagement.Core.Enums;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using BookManagement.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BookManagement.Application.BackgroundServices;
public class BeforeLateLoanNotificationService : BackgroundService
{
    private readonly IEmailService _emailService;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public BeforeLateLoanNotificationService(IEmailService emailService, IServiceScopeFactory serviceScopeFactory)
    {
        _emailService = emailService;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var returnDate = DateTime.Today.AddDays(1);

            using var scope = _serviceScopeFactory.CreateAsyncScope();
            var loanRepository = scope.ServiceProvider.GetRequiredService<ILoanRepository>();

            var loans = await loanRepository.GetAllLoansAsync();
            loans = loans.Where(l => l.Status != LoanStatus.Returned && l.ReturnDate.Date == returnDate.Date).ToList();

            if (!loans.IsNullOrEmpty())
            {
                string subject = "BookLoanManagement - You have a loan close to return";

                foreach (var loan in loans)
                {
                    string message = $"Hi {loan.User.Name}, your loan for book {loan.Book.Title} must be returned tomorrow. Thanks!";

                    await _emailService.SendEmailAsync(loan.User.Name, loan.User.Email, subject, message);
                }
            }

            await Task.Delay(TimeSpan.FromHours(24));
        }
    }
}
