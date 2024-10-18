using BookManagement.Application.Commands.UpdateBookStock;
using FluentValidation;

namespace BookManagement.Application.Validators;
public class UpdateBookStockValidator : AbstractValidator<UpdateBookStockCommand>
{
    public UpdateBookStockValidator()
    {
        RuleFor(s => s.BookId).NotEmpty().WithMessage("The BookId cannot be empty or zero.");
        RuleFor(s => s.BookId).GreaterThan(0).WithMessage("Invalid Book Id.");

        RuleFor(s => s.Quantity).GreaterThan(0).WithMessage("Quantiy cannot be less than 0.");
    }
}
