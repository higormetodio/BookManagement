using BookManagement.Application.Commands.CreateUser;
using FluentValidation;

namespace BookManagement.Application.Validators;
public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(u => u.Name).NotEmpty().WithMessage("Invalid Name. Name is required.")
                            .MaximumLength(100);

        RuleFor(u => u.Email).EmailAddress().WithMessage("Invalid Email.");
    }
}
