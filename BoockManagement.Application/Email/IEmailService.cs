namespace BookManagement.Application.Email;
public interface IEmailService
{
    Task SendEmailAsync(string toName, string toEmail, string subject = "BookMnagement - Loan close to return.",
                        string message = $"Your loan should be retuned tomorrow", string fromEmail = "higor.metodio@outlook.com");
}
