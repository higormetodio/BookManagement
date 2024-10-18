using BookManagement.Application.Commands.CreateUser;
using BookManagement.Application.Commands.UpdateUser;
using BookManagement.Application.Validators;
using FluentValidation.TestHelper;

namespace BookManagement.UnitTests.Application.Validators;
public class UpdateUserValidatorTests
{
    private readonly UpdateUserValidator _validator;

    public UpdateUserValidatorTests()
    {
        _validator = new UpdateUserValidator();
    }

    [Fact]
    public async Task Should_Fail_Returning_Message_For_UpdateUserValidator_When_Empty_Id()
    {
        //Arrange
        var userUpdateUserCommand = new UpdateUserCommand
        {
            Name = "",
            Email = "rafael@gmail.com",
            BirthDate = new DateTime(1987, 7, 3),
            Password = "1Q2w3e4R#!"
        };

        //Act
        var result = await _validator.TestValidateAsync(userUpdateUserCommand);

        //Assert
        result.ShouldHaveValidationErrorFor(u => u.Id)
              .WithErrorMessage("The Id cannot be empty or zero.");
    }

    [Fact]
    public async Task Should_Fail_Returning_Message_For_UpdateUserValidator_When_Invalid_Id()
    {
        //Arrange
        var userUpdateUserCommand = new UpdateUserCommand
        {
            Id = -1,
            Name = "Rafael Oliveira",
            Email = "rafael@gmail.com",
            BirthDate = new DateTime(1987, 7, 3),
            Password = "1Q2w3e4R#!"
        };

        //Act
        var result = await _validator.TestValidateAsync(userUpdateUserCommand);

        //Assert
        result.ShouldHaveValidationErrorFor(u => u.Id)
              .WithErrorMessage("Invalid Id.");
    }

    [Fact]
    public async Task Should_Fail_Returning_Message_For_UpdateUserValidator_When_Empty_Name()
    {
        //Arrange
        var userUpdateUserCommand = new UpdateUserCommand
        {
            Id = 1,
            Name = "",
            Email = "rafael@gmail.com",
            BirthDate = new DateTime(1987, 7, 3),
            Password = "1Q2w3e4R#!"
        };

        //Act
        var result = await _validator.TestValidateAsync(userUpdateUserCommand);

        //Assert
        result.ShouldHaveValidationErrorFor(u => u.Name)
              .WithErrorMessage("Invalid Name. Name is required.");
    }

    [Fact]
    public async Task Should_Fail_Returning_Message_For_UpdateUserValidator_When_Above_Max_Size_Name()
    {
        //Arrange
        var userUpdateUserCommand = new UpdateUserCommand
        {
            Id = 1,
            Name = "Rafael Oliveira000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000",
            Email = "rafael@gmail.com",
            BirthDate = new DateTime(1987, 7, 3),
            Password = "1Q2w3e4R#!"
        };

        //Act
        var result = await _validator.TestValidateAsync(userUpdateUserCommand);

        //Assert
        result.ShouldHaveValidationErrorFor(u => u.Name)
              .WithErrorMessage("The maximum field size for Name is 100.");
    }

    [Fact]
    public async Task Should_Fail_Returning_Message_For_UpdateUserValidator_When_Empty_Email()
    {
        //Arrange
        var userUpdateUserCommand = new UpdateUserCommand
        {
            Id = 1,
            Name = "Rafael Oliveira",
            Email = "",
            BirthDate = new DateTime(1987, 7, 3),
            Password = "1Q2w3e4R#!"
        };

        //Act
        var result = await _validator.TestValidateAsync(userUpdateUserCommand);

        //Assert
        result.ShouldHaveValidationErrorFor(u => u.Email)
              .WithErrorMessage("Invalid Email. Email is required.");
    }

    [Fact]
    public async Task Should_Fail_Returning_Message_For_UpdateUserValidator_When_Invalid_Email()
    {
        //Arrange
        var userUpdateUserCommand = new UpdateUserCommand
        {
            Id = 1,
            Name = "Rafael Oliveira",
            Email = "rafael.gmail.com",
            BirthDate = new DateTime(1987, 7, 3),
            Password = "1Q2w3e4R#!"
        };

        //Act
        var result = await _validator.TestValidateAsync(userUpdateUserCommand);

        //Assert
        result.ShouldHaveValidationErrorFor(u => u.Email)
              .WithErrorMessage("Invalid Email. Incorrect format for email.");
    }

    [Fact]
    public async Task Should_Fail_Returning_Message_For_UpdateUserValidator_When_Empty_Password()
    {
        //Arrange
        var userUpdateUserCommand = new UpdateUserCommand
        {
            Id = 1,
            Name = "Rafael Oliveira",
            Email = "rafael@gmail.com",
            BirthDate = new DateTime(1987, 7, 3),
            Password = ""
        };

        //Act
        var result = await _validator.TestValidateAsync(userUpdateUserCommand);

        //Assert
        result.ShouldHaveValidationErrorFor(u => u.Password)
              .WithErrorMessage("Invalid Password. Password is required.");
    }

    [Fact]
    public async Task Should_Fail_Returning_Message_For_UpdateUserValidator_When_Invalid_Password()
    {
        //Arrange
        var userUpdateUserCommand = new UpdateUserCommand
        {
            Id = 1,
            Name = "Rafael Oliveira",
            Email = "rafael@gmail.com",
            BirthDate = new DateTime(1987, 7, 3),
            Password = "1234"
        };

        //Act
        var result = await _validator.TestValidateAsync(userUpdateUserCommand);

        //Assert
        result.ShouldHaveValidationErrorFor(u => u.Password)
              .WithErrorMessage("Password must contain at least 8 characters, one number, one capital letter, one lowercase letter, and one special character.");
    }

    [Fact]
    public async Task Should_Fail_Returning_Message_For_UpdateUserValidator_When_Empty_BirthDate()
    {
        //Arrange
        var userUpdateUserCommand = new UpdateUserCommand
        {
            Id = 1,
            Name = "Rafael Oliveira",
            Email = "rafael@gmail.com",
            Password = ""
        };

        //Act
        var result = await _validator.TestValidateAsync(userUpdateUserCommand);

        //Assert
        result.ShouldHaveValidationErrorFor(u => u.BirthDate)
              .WithErrorMessage("Invalid Birth Date. Birth Date is required.");
    }

    [Fact]
    public async Task Should_Fail_Returning_Message_For_UpdateUserValidator_When_Below_Permitted_BirthDate()
    {
        //Arrange
        var userUpdateUserCommand = new UpdateUserCommand
        {
            Id = 1,
            Name = "Rafael Oliveira",
            Email = "rafael@gmail.com",
            BirthDate = new DateTime(1800, 7, 3),
            Password = "1234"
        };

        //Act
        var result = await _validator.TestValidateAsync(userUpdateUserCommand);

        //Assert
        result.ShouldHaveValidationErrorFor(u => u.BirthDate)
              .WithErrorMessage($"The Birth Date should be between 1900 and {DateTime.Now:MM-dd-yyyy}");
    }

    [Fact]
    public async Task Should_Fail_Returning_Message_For_UpdateUserValidator_When_Above_Permitted_BirthDate()
    {
        //Arrange
        var userUpdateUserCommand = new UpdateUserCommand
        {
            Id = 1,
            Name = "Rafael Oliveira",
            Email = "rafael@gmail.com",
            BirthDate = DateTime.Now.AddYears(2),
            Password = "1234"
        };

        //Act
        var result = await _validator.TestValidateAsync(userUpdateUserCommand);

        //Assert
        result.ShouldHaveValidationErrorFor(u => u.BirthDate)
              .WithErrorMessage($"The Birth Date should be between 1900 and {DateTime.Now:MM-dd-yyyy}");
    }

    [Fact]
    public async Task Should_Not_Fail_Without_Message__When_Valid_UpdateUserValidator_Properties()
    {
        //Arrange
        var userUpdateUserCommand = new UpdateUserCommand
        {
            Id = 1,
            Name = "Rafael Oliveira",
            Email = "rafael@gmail.com",
            BirthDate = new DateTime(1987, 7, 3),
            Password = "1Q2w3e4R#!"
        };

        //Act
        var result = await _validator.TestValidateAsync(userUpdateUserCommand);

        //Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
