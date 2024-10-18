using BookManagement.Application.Commands.UpdateBook;
using FluentValidation;

namespace BookManagement.Application.Validators;
public class UpdateBookValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookValidator()
    {
        RuleFor(b => b.Id).NotEmpty().WithMessage("The Id cannot be empty or zero.");
        RuleFor(b => b.Id).GreaterThan(0).WithMessage("Invalid Id.");

        RuleFor(b => b.Title).NotEmpty().WithMessage("Invalid Title. Title is required.")
                             .MaximumLength(200).WithMessage("The maximum field size for Title is 200.");

        RuleFor(b => b.Author).NotEmpty().WithMessage("Invalid Author. Author is required.")
                              .MaximumLength(100).WithMessage("The maximum field size for Author is 100.");

        RuleFor(b => b.ISBN).NotEmpty().WithMessage("Invalid ISBN. ISBN is required.")
                            .MaximumLength(20).WithMessage("The maximum field size for ISBN is 20.");

        RuleFor(b => b.PublicationYear).InclusiveBetween(1500, DateTime.Now.Year).WithMessage($"Invalid Publication Year. The Publication Year is not in the range between 1500 and {DateTime.Now.Year}.");
    }
}
