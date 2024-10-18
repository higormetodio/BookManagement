using BookManagement.Application.Commands.UpdateBookStock;
using BookManagement.Application.Validators;
using FluentValidation.TestHelper;

namespace BookManagement.UnitTests.Application.Validators;
public class UpdateBookStockValidatorTests
{
    private readonly UpdateBookStockValidator _validator;

    public UpdateBookStockValidatorTests()
    {
        _validator = new UpdateBookStockValidator();
    }

    [Fact]
    public async Task Should_Fail_Returning_Message_For_UpdateBookStockValidator_When_Empty_BookId()
    {
        //Arrange
        var bookUpdateBookStockCommand = new UpdateBookStockCommand
        {
            Quantity = 5
        };

        //Act
        var result = await _validator.TestValidateAsync(bookUpdateBookStockCommand);

        //Assert
        result.ShouldHaveValidationErrorFor(bs => bs.BookId)
              .WithErrorMessage("The BookId cannot be empty or zero.");
    }

    [Fact]
    public async Task Should_Fail_Returning_Message_For_UpdateBookStockValidator_When_BookId_Less_Than_Zero()
    {
        //Arrange
        var bookUpdateBookStockCommand = new UpdateBookStockCommand
        {
            BookId = -1,
            Quantity = 5
        };

        //Act
        var result = await _validator.TestValidateAsync(bookUpdateBookStockCommand);

        //Assert
        result.ShouldHaveValidationErrorFor(bs => bs.BookId)
              .WithErrorMessage("Invalid Book Id.");
    }

    [Fact]
    public async Task Should_Fail_Returning_Message_For_UpdateBookStockValidator_When_Quantity_Less_Than_Zero()
    {
        //Arrange
        var bookUpdateBookStockCommand = new UpdateBookStockCommand
        {
            BookId = 1,
            Quantity = -5
        };

        //Act
        var result = await _validator.TestValidateAsync(bookUpdateBookStockCommand);

        //Assert
        result.ShouldHaveValidationErrorFor(bs => bs.Quantity)
              .WithErrorMessage("Quantiy cannot be less than 0.");
    }

    [Fact]
    public async Task Should_Not_Fail_Without_Message_When_Valid_UpdateBookStockCommand_Properties()
    {
        //Arrange
        var bookUpdateBookStockCommand = new UpdateBookStockCommand
        {
            BookId = 1,
            Quantity = 5
        };

        //Act
        var result = await _validator.TestValidateAsync(bookUpdateBookStockCommand);

        //Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
