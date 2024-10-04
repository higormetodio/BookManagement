using BookManagement.Application.Commands.CreateLoan;
using FluentValidation;

namespace BookManagement.Application.Validators;
public class CreateLoanValidator : AbstractValidator<CreateLoanCommand>
{
    public CreateLoanValidator()
    {
        RuleFor(l => l.BookId).NotEmpty().WithMessage("The BookId cannot be empty.");
        RuleFor(l => l.BookId).GreaterThanOrEqualTo(0).WithMessage("Invalid BookId.");

        RuleFor(l => l.UserId).NotEmpty().WithMessage("The UserId cannot be empty.");
        RuleFor(l => l.UserId).GreaterThanOrEqualTo(0).WithMessage("Invalid UserId.");
    }
}
