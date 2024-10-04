using BookManagement.Application.Commands.UpdateUser;
using FluentValidation;

namespace BookManagement.Application.Validators;
public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(u => u.Id).NotEmpty().WithMessage("The Id cannot be empty.");
        RuleFor(u => u.Id).GreaterThanOrEqualTo(0).WithMessage("Invalid Id.");

        RuleFor(u => u.Name).NotEmpty().WithMessage("Invalid Name. Name is required.")
                            .MaximumLength(100);

        RuleFor(u => u.Email).EmailAddress().WithMessage("Invalid Email.");
    }
}
