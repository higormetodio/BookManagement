using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace BookManagement.Application.Email;
public class EmailService : IEmailService
{
    private readonly SmtpSettings _smtpSettings;

    public EmailService(IOptions<SmtpSettings> smtpSettings)
    {
        _smtpSettings = smtpSettings.Value;
    }
    public async Task SendEmailAsync(string toName, string toEmail, string subject = "BookMnagement - Loan close to return.", string message = "Your loan should be retuned tomorrow", string fromEmail = "higor.metodio@outlook.com")
    {
        var host = _smtpSettings.Host;
        var port = _smtpSettings.Port;
        var userName = _smtpSettings.UserName;
        var password = _smtpSettings.Password;

        var mail = new MimeMessage();
        mail.From.Add(new MailboxAddress(userName, fromEmail));
        mail.To.Add(new MailboxAddress(toName, toEmail));
        mail.Subject = subject;
        mail.Body = new TextPart("html")
        {
            Text = message
        };

        using var smtpClient = new SmtpClient();
        await smtpClient.ConnectAsync(host, port);
        await smtpClient.AuthenticateAsync(userName, password);
        await smtpClient.SendAsync(mail);
        await smtpClient.DisconnectAsync(true);
    }
}
