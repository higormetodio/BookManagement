using BookManagement.Application.Commands.CreateLoan;
using BookManagement.Application.Validators;
using FluentValidation.TestHelper;

namespace BookManagement.UnitTests.Application.Validators;
public class CreateLoanValidatorTests
{
    private readonly CreateLoanValidator _validator;

    public CreateLoanValidatorTests()
    {
        _validator = new CreateLoanValidator();
    }

    [Fact]
    public async Task Should_Fail_Returning_Message_For_CreateLoanValidator_When_BookId_Empty()
    {
        //Arrange
        var loanCreateLoanCommand = new CreateLoanCommand
        {
            UserId = 3
        };

        //Act
        var result = await _validator.TestValidateAsync(loanCreateLoanCommand);

        //Assert
        result.ShouldHaveValidationErrorFor(l => l.BookId)
              .WithErrorMessage("The BookId cannot be empty or zero.");
    }

    [Fact]
    public async Task Should_Fail_Returning_Message_For_CreateLoanValidator_When_BookId_Less_Than_Zero()
    {
        //Arrange
        var loanCreateLoanCommand = new CreateLoanCommand
        {
            UserId = 3,
            BookId = -1
        };

        //Act
        var result = await _validator.TestValidateAsync(loanCreateLoanCommand);

        //Assert
        result.ShouldHaveValidationErrorFor(l => l.BookId)
              .WithErrorMessage("Invalid BookId.");
    }

    [Fact]
    public async Task Should_Fail_Returning_Message_For_CreateLoanValidator_When_UserId_Empty()
    {
        //Arrange
        var loanCreateLoanCommand = new CreateLoanCommand
        {
            BookId = 1
        };

        //Act
        var result = await _validator.TestValidateAsync(loanCreateLoanCommand);

        //Assert
        result.ShouldHaveValidationErrorFor(l => l.UserId)
              .WithErrorMessage("The UserId cannot be empty or zero.");
    }

    [Fact]
    public async Task Should_Fail_Returning_Message_For_CreateLoanValidator_When_UserId_Less_Than_Zero()
    {
        //Arrange
        var loanCreateLoanCommand = new CreateLoanCommand
        {
            UserId = -3,
            BookId = 1
        };

        //Act
        var result = await _validator.TestValidateAsync(loanCreateLoanCommand);

        //Assert
        result.ShouldHaveValidationErrorFor(l => l.UserId)
              .WithErrorMessage("Invalid UserId.");
    }

    [Fact]
    public async Task Should_Not_Fail_Without_Message_When_Valid_CreateLoanCommand_Properties()
    {
        //Arrange
        var loanCreateLoanCommand = new CreateLoanCommand
        {
            UserId = 3,
            BookId = 1
        };

        //Act
        var result = await _validator.TestValidateAsync(loanCreateLoanCommand);

        //Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
