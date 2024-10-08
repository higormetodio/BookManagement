using BookManagement.Application.Commands.CreateUser;
using FluentValidation;
using System.Text.RegularExpressions;

namespace BookManagement.Application.Validators;
public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(u => u.Name).NotEmpty().WithMessage("Invalid Name. Name is required.")
                            .MaximumLength(100);

        RuleFor(u => u.Email).EmailAddress().WithMessage("Invalid Email.");

        RuleFor(u => u.Password).Must(ValidPassword)
                                .WithMessage("Password must contain at least 8 characters, one number, one capital letter, one lowercase letter, and one special character.");

        RuleFor(u => u.BirthDate).GreaterThanOrEqualTo(new DateTime(1900, 1, 1));
    }

    public bool ValidPassword(string password)
    {
        var regex = new Regex(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$");

        return regex.IsMatch(password);
    }
}
