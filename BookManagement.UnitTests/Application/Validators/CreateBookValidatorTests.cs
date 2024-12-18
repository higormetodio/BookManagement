﻿using BookManagement.Application.Commands.CreateBook;
using BookManagement.Application.Validators;
using FluentValidation.TestHelper;

namespace BookManagement.UnitTests.Application.Validators;
public class CreateBookValidatorTests
{
    private readonly CreateBookValidator _validator;

    public CreateBookValidatorTests()
    {
        _validator = new CreateBookValidator();
    }

    [Fact]
    public async Task Should_Fail_Returning_Message_For_CreateBookValidator_When_Empty_Title()
    {
        //Arrange
        var bookCreateBookCommand = new CreateBookCommand
        {
            Title = "",
            Author = "Andrew S. Tanenbaum",
            ISBN = "B00VQGOWH4",
            PublicationYear = 2015
        };

        //Act
        var result = await _validator.TestValidateAsync(bookCreateBookCommand);

        //Assert
        result.ShouldHaveValidationErrorFor(b => b.Title)
              .WithErrorMessage("Invalid Title. Title is required.");
    }

    [Fact]
    public async Task Should_Fail_Returning_Message_For_CreateBookValidator_When_Invalid_Title()
    {
        //Arrange
        var bookCreateBookCommand = new CreateBookCommand
        {
            Title = "Sistemas distribuídos: princípios e paradigmas0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000",
            Author = "Andrew S. Tanenbaum",
            ISBN = "B00VQGOWH4",
            PublicationYear = 2015
        };

        //Act
        var result = await _validator.TestValidateAsync(bookCreateBookCommand);

        //Assert
        result.ShouldHaveValidationErrorFor(b => b.Title)
              .WithErrorMessage("The maximum field size for Title is 200.");
    }

    [Fact]
    public async Task Should_Fail_Returning_Message_For_CreateBookValidator_When_Empty_Author()
    {
        //Arrange
        var bookCreateBookCommand = new CreateBookCommand
        {
            Title = "Sistemas distribuídos: princípios e paradigmas",
            Author = "",
            ISBN = "B00VQGOWH4",
            PublicationYear = 2015
        };

        //Act
        var result = await _validator.TestValidateAsync(bookCreateBookCommand);

        //Assert
        result.ShouldHaveValidationErrorFor(b => b.Author)
              .WithErrorMessage("Invalid Author. Author is required.");
    }

    [Fact]
    public async Task Should_Fail_Returning_Message_For_CreateBookValidator_When_Invalid_Author()
    {
        //Arrange
        var bookCreateBookCommand = new CreateBookCommand
        {
            Title = "Sistemas distribuídos: princípios e paradigmas",
            Author = "Andrew S. Tanenbaumooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo",
            ISBN = "B00VQGOWH4",
            PublicationYear = 2015
        };

        //Act
        var result = await _validator.TestValidateAsync(bookCreateBookCommand);

        //Assert
        result.ShouldHaveValidationErrorFor(b => b.Author)
              .WithErrorMessage("The maximum field size for Author is 100.");
    }

    [Fact]
    public async Task Should_Fail_Returning_Message_For_CreateBookValidator_When_Empty_ISBN()
    {
        //Arrange
        var bookCreateBookCommand = new CreateBookCommand
        {
            Title = "Sistemas distribuídos: princípios e paradigmas",
            Author = "Andrew S. Tanenbaum",
            ISBN = "",
            PublicationYear = 2015
        };

        //Act
        var result = await _validator.TestValidateAsync(bookCreateBookCommand);

        //Assert
        result.ShouldHaveValidationErrorFor(b => b.ISBN)
              .WithErrorMessage("Invalid ISBN. ISBN is required.");
    }

    [Fact]
    public async Task Should_Fail_Returning_Message_For_CreateBookValidator_When_Invalid_ISBN()
    {
        //Arrange
        var bookCreateBookCommand = new CreateBookCommand
        {
            Title = "Sistemas distribuídos: princípios e paradigmas",
            Author = "Andrew S. Tanenbaum",
            ISBN = "B00VQGOWH400000000000000000000000000000000000000000000000000000000000000000000000000000000000000",
            PublicationYear = 2015
        };

        //Act
        var result = await _validator.TestValidateAsync(bookCreateBookCommand);

        //Assert
        result.ShouldHaveValidationErrorFor(b => b.ISBN)
              .WithErrorMessage("The maximum field size for ISBN is 20.");
    }

    [Fact]
    public async Task Should_Fail_Returning_Message_For_CreateBookValidator_When_Invalid_PublicationYear_Above_DateTimeNow()
    {
        //Arrange
        var bookCreateBookCommand = new CreateBookCommand
        {
            Title = "Sistemas distribuídos: princípios e paradigmas",
            Author = "Andrew S. Tanenbaum",
            ISBN = "B00VQGOWH4",
            PublicationYear = 2026
        };

        //Act
        var result = await _validator.TestValidateAsync(bookCreateBookCommand);

        //Assert
        result.ShouldHaveValidationErrorFor(b => b.PublicationYear)
              .WithErrorMessage($"Invalid Publication Year. The Publication Year is not in the range between 1500 and {DateTime.Now.Year}.");
    }

    [Fact]
    public async Task Should_Fail_Returning_Message_For_CreateBookValidator_When_Invalid_PublicationYear_Below_DateTimeNow()
    {
        //Arrange
        var bookCreateBookCommand = new CreateBookCommand
        {
            Title = "Sistemas distribuídos: princípios e paradigmas",
            Author = "Andrew S. Tanenbaum",
            ISBN = "B00VQGOWH4",
            PublicationYear = 1400
        };

        //Act
        var result = await _validator.TestValidateAsync(bookCreateBookCommand);

        //Assert
        result.ShouldHaveValidationErrorFor(b => b.PublicationYear)
              .WithErrorMessage($"Invalid Publication Year. The Publication Year is not in the range between 1500 and {DateTime.Now.Year}.");
    }

    [Fact]
    public async Task Should_Not_Fail_Without_Message_When_Valid_CreateBookCommand_Properties()
    {
        //Arrange
        var bookCreateBookCommand = new CreateBookCommand
        {
            Title = "Sistemas distribuídos: princípios e paradigmas",
            Author = "Andrew S. Tanenbaum",
            ISBN = "B00VQGOWH4",
            PublicationYear = 2015
        };

        //Act
        var result = await _validator.TestValidateAsync(bookCreateBookCommand);

        //Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
