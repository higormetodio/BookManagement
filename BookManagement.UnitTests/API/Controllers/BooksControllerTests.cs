using BookManagement.API.Controllers;
using BookManagement.Application.Commands.CreateBook;
using BookManagement.Application.Commands.UpdateBook;
using BookManagement.Application.Commands.UpdateBookOnlyActive;
using BookManagement.Application.Commands.UpdateBookStock;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
namespace BookManagement.UnitTests.API.Controllers;
public class BooksControllerTests
{
    private readonly IMediator _fakeMediator;
    private readonly BooksController _booksController;

    public BooksControllerTests()
    {
        _fakeMediator = new FakeMediatorBook();
        _booksController = new BooksController(_fakeMediator);
    }

    [Fact]
    public async Task Should_Retrn_NotFound_Whend_Get_Book_Search_Criteria_Not_Met()
    {
        //Arrange

        //Act
        var result = await _booksController.Get("Refatorando");

        //Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task Should_Retrn_NotFound_Whend_Get_Book_Search_Criteria_Is_Inactive_Book()
    {
        //Arrange

        //Act
        var result = await _booksController.Get("Sistemas");

        //Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task Should_Retrn_NotFound_Whend_Get_Unregistred_Book_Id()
    {
        //Arrange

        //Act
        var result = await _booksController.GetById(10);

        //Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task Should_Retrn_NotFound_Whend_Get_Registred_Book_Id_Is_Inactive_Book()
    {
        //Arrange

        //Act
        var result = await _booksController.GetById(4);

        //Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task Should_Retrn_NotFound_Whend_Get_Unregistred_Book_Id_With_Loans()
    {
        //Arrange

        //Act
        var result = await _booksController.GetBookByIdLoans(10);

        //Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task Should_Retrn_NotFound_Whend_Get_Registred_Book_Id_With_Loans_Inactive_Book()
    {
        //Arrange

        //Act
        var result = await _booksController.GetBookByIdLoans(4);

        //Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task Should_Return_BadRequest_Whend_Put_Book_With_Unregistred_Id()
    {
        //Arrange
        var updateBookCommand = new UpdateBookCommand
        {
            BookId = 10,
            Title = "Sistemas distribuídos: princípios e paradigmas",
            Author = "Andrew S. Tanenbaum",
            ISBN = "B00VQGOWH4",
            PublicationYear = 2015
        };
        //Act
        var result = await _booksController.Put(updateBookCommand);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Should_Return_BadRequest_Whend_Put_Book_With_Inactive_Book()
    {
        //Arrange
        var updateBookCommand = new UpdateBookCommand
        {
            BookId = 4,
            Title = "Sistemas distribuídos: princípios e paradigmas",
            Author = "Andrew S. Tanenbaum",
            ISBN = "B00VQGOWH4",
            PublicationYear = 2015
        };
        //Act
        var result = await _booksController.Put(updateBookCommand);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Should_Return_BadRequest_Whend_Put_BookStock_With_Unregistred_Id()
    {
        //Arrange
        var updateBookStockCommand = new UpdateBookStockCommand
        {
            BookId = 10,
            Quantity = 5
        };

        //Act
        var result = await _booksController.Put(updateBookStockCommand);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Should_Return_BadRequest_Whend_Put_BookStock_With_Inactive_Book()
    {
        //Arrange
        var updateBookStockCommand = new UpdateBookStockCommand
        {
            BookId = 4,
            Quantity = 5
        };

        //Act
        var result = await _booksController.Put(updateBookStockCommand);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Should_Return_BadRequest_Whend_Patch_Book_With_Null_JsonPatchDocument()
    {
        //Arrange
        JsonPatchDocument<UpdateBookOnlyActiveCommand> patchBook = null;

        //Act
        var result = await _booksController.Patch(4, patchBook);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Should_Return_BadRequest_Whend_Patch_Book_With_Invalid_Id()
    {
        //Arrange
        var patchBook = new JsonPatchDocument<UpdateBookOnlyActiveCommand>();

        //Act
        var result = await _booksController.Patch(0, patchBook);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Should_Return_BadRequest_Whend_Patch_Book_Propertie_Active_When_Id_Unregistred()
    {
        //Arrange
        var patchBook = new JsonPatchDocument<UpdateBookOnlyActiveCommand>();

        //Act
        var result = await _booksController.Patch(10, patchBook);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Should_Return_BadRequest_Whend_Patch_Book_Propertie_Active_When_Already_Active()
    {
        //Arrange
        var patchBook = new JsonPatchDocument<UpdateBookOnlyActiveCommand>();

        //Act
        var result = await _booksController.Patch(1, patchBook);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Should_Return_BadRequest_Whend_Delete_Book_When_Id_Unregistred()
    {
        //Arrange

        //Act
        var result = await _booksController.Delete(10);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Should_Return_BadRequest_Whend_Delete_Book_When_Book_Inactive()
    {
        //Arrange

        //Act
        var result = await _booksController.Delete(4);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Should_Return_BadRequest_Whend_Delete_Book_When_Book_Has_Loans_Unreturned()
    {
        //Arrange

        //Act
        var result = await _booksController.Delete(2);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Should_Return_Ok_Whend_Get_Book_Search_Criteria_Met()
    {
        //Arrange

        //Act
        var result = await _booksController.Get("Computadores");

        //Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Should_Return_Ok_Whend_Get_Registred_Book_Id()
    {
        //Arrange

        //Act
        var result = await _booksController.GetById(1);

        //Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Should_Return_Ok_Whend_Get_Registred_Book_Id_With_Loans()
    {
        //Arrange

        //Act
        var result = await _booksController.GetBookByIdLoans(1);

        //Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Should_Return_CreatedAtActionResult_Whend_Post_Book()
    {
        //Arrange
        var command = new CreateBookCommand
        {
            Title = "Sistemas distribuídos: princípios e paradigmas",
            Author = "Andrew S. Tanenbaum",
            ISBN = "B00VQGOWH4",
            PublicationYear = 2015
        };

        //Act
        var result = await _booksController.Post(command);

        //Assert
        Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public async Task Should_Return_NoContent_Whend_Put_Book()
    {
        //Arrange
        var updateBookCommand = new UpdateBookCommand
        {
            BookId = 1,
            Title = "Sistemas distribuídos: princípios e paradigmas",
            Author = "Andrew S. Tanenbaum",
            ISBN = "B00VQGOWH4",
            PublicationYear = 2015
        };

        //Act
        var result = await _booksController.Put(updateBookCommand);

        //Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Should_Return_NoContent_Whend_Put_BookStock()
    {
        //Arrange
        var updateBookStockCommand = new UpdateBookStockCommand
        {
            BookId = 1,
            Quantity = 5
        };

        //Act
        var result = await _booksController.Put(updateBookStockCommand);

        //Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Should_Return_NoContent_Whend_Patch_Book_Propertie_Active()
    {
        //Arrange
        var patchBook = new JsonPatchDocument<UpdateBookOnlyActiveCommand>();

        //Act
        var result = await _booksController.Patch(4, patchBook);

        //Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Should_Return_NoContent_Whend_Delete_Book_Inativating_It()
    {
        //Arrange        

        //Act
        var result = await _booksController.Delete(1);

        //Assert
        Assert.IsType<NoContentResult>(result);
    }
}
