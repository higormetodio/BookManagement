using BookManagement.API.Controllers;
using BookManagement.Application.Commands.CreateBook;
using BookManagement.Application.Commands.CreateLoan;
using BookManagement.Application.Commands.ReturnLoan;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.UnitTests.API.Controllers;
public class LoansControllerTests
{
    private readonly IMediator _fakeMediator;
    private readonly LoansController _loansController;

    public LoansControllerTests()
    {
        _fakeMediator = new FakeMediatorLoan();
        _loansController = new LoansController(_fakeMediator);
    }

    [Fact]
    public async Task Should_Retrn_NotFound_Whend_Get_Loan_Search_Criteria_Not_Met()
    {
        //Arrange

        //Act
        var result = await _loansController.Get("Refatorando");

        //Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task Should_Retrn_NotFound_Whend_Get_Unregistred_Loan_Id()
    {
        //Arrange

        //Act
        var result = await _loansController.GetById(10);

        //Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task Should_Retrn_NotFound_Whend_Get_Loan_Id_Returned()
    {
        //Arrange

        //Act
        var result = await _loansController.GetById(4);

        //Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task Should_Retrn_BadRequest_Whend_Post_Loan_With_Unregistred_UserId()
    {
        //Arrange
        var createLoanCommand = new CreateLoanCommand
        {
            UserId = 10,
            BookId = 1
        };

        //Act
        var result = await _loansController.Post(createLoanCommand);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Should_Retrn_BadRequest_Whend_Post_Loan_With_Inactive_UserId()
    {
        //Arrange
        var createLoanCommand = new CreateLoanCommand
        {
            UserId = 4,
            BookId = 1
        };

        //Act
        var result = await _loansController.Post(createLoanCommand);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Should_Retrn_BadRequest_Whend_Post_Loan_With_Unregistred_BookId()
    {
        //Arrange
        var createLoanCommand = new CreateLoanCommand
        {
            UserId = 2,
            BookId = 15
        };

        //Act
        var result = await _loansController.Post(createLoanCommand);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Should_Retrn_BadRequest_Whend_Post_Loan_With_Inactive_BookId()
    {
        //Arrange
        var createLoanCommand = new CreateLoanCommand
        {
            UserId = 2,
            BookId = 4
        };

        //Act
        var result = await _loansController.Post(createLoanCommand);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Should_Retrn_BadRequest_Whend_Post_Loan_With_Already_Loaned_Book()
    {
        //Arrange
        var createLoanCommand = new CreateLoanCommand
        {
            UserId = 2,
            BookId = 2
        };

        //Act
        var result = await _loansController.Post(createLoanCommand);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Should_Retrn_BadRequest_Whend_Post_Loan_With_Zero_Book_Stock()
    {
        //Arrange
        var createLoanCommand = new CreateLoanCommand
        {
            UserId = 2,
            BookId = 3
        };

        //Act
        var result = await _loansController.Post(createLoanCommand);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Should_Retrn_BadRequest_Whend_Put_Loan_With_Unregister_Loan_Id()
    {
        //Arrange

        //Act
        var result = await _loansController.Put(10);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Should_Retrn_BadRequest_Whend_Put_Loan_With_Loan_Id_Already_Returned()
    {
        //Arrange

        //Act
        var result = await _loansController.Put(4);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Should_Return_CreatedAtActionResult_Whend_Post_Loan()
    {
        //Arrange
        var createLoanCommand = new CreateLoanCommand
        {
            UserId = 3,
            BookId = 1
        };

        //Act
        var result = await _loansController.Post(createLoanCommand);

        //Assert
        Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public async Task Should_Return_NoContent_Whend_Put_Loan()
    {
        //Arrange

        //Act
        var result = await _loansController.Put(1);

        //Assert
        Assert.IsType<NoContentResult>(result);
    }
}
