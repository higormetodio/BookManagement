using BookManagement.Application.Commands.UpdateUser;
using FluentValidation;
using System.Text.RegularExpressions;

namespace BookManagement.Application.Validators;
public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(u => u.UserId).NotEmpty().WithMessage("The Id cannot be empty or zero.");
        RuleFor(u => u.UserId).GreaterThanOrEqualTo(0).WithMessage("Invalid Id.");

        RuleFor(u => u.Name).NotEmpty().WithMessage("Invalid Name. Name is required.")
                            .MaximumLength(100).WithMessage("The maximum field size for Name is 100.");

        RuleFor(u => u.Email).NotEmpty().WithMessage("Invalid Email. Email is required.")
                             .EmailAddress().WithMessage("Invalid Email. Incorrect format for email.");

        RuleFor(u => u.Password).NotEmpty().WithMessage("Invalid Password. Password is required.")
                                .Must(ValidPassword).WithMessage("Password must contain at least 8 characters, one number, one capital letter, one lowercase letter, and one special character.");

        RuleFor(u => u.BirthDate).NotEmpty().WithMessage("Invalid Birth Date. Birth Date is required.")
                                 .InclusiveBetween(new DateTime(1900, 1, 1), DateTime.Now).WithMessage($"The Birth Date should be between 1900 and {DateTime.Now:MM-dd-yyyy}");
    }

    public bool ValidPassword(string password)
    {
        var regex = new Regex(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$");

        return regex.IsMatch(password);
    }
}
