using BookManagement.API.Controllers;
using BookManagement.Application.Commands.UpdateUser;
using BookManagement.Application.Commands.UpdateUserOnlyActive;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.UnitTests.API.Controllers;
public class UsersControllerTests
{
    private readonly IMediator _fakeMediator;
    private readonly UsersController _userController;

    public UsersControllerTests()
    {
        _fakeMediator = new FakeMediatorUser();
        _userController = new UsersController(_fakeMediator);
    }

    [Fact]
    public async Task Should_Retrn_NotFound_Whend_Get_User_Search_Criteria_Not_Met()
    {
        //Arrange

        //Act
        var result = await _userController.Get("Pereira");

        //Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task Should_Retrn_NotFound_Whend_Get_User_Search_Criteria_Is_Inactive_User()
    {
        //Arrange

        //Act
        var result = await _userController.Get("Alexandre");

        //Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task Should_Retrn_NotFound_Whend_Get_Unregistred_User_Id()
    {
        //Arrange

        //Act
        var result = await _userController.GetById(10);

        //Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task Should_Retrn_NotFound_Whend_Get_Registred_User_Id_Is_Inactive_User()
    {
        //Arrange

        //Act
        var result = await _userController.GetById(4);

        //Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task Should_Retrn_NotFound_Whend_Get_Unregistred_User_Id_With_Loans()
    {
        //Arrange

        //Act
        var result = await _userController.GetUserByIdLoans(10);

        //Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task Should_Retrn_NotFound_Whend_Get_Registred_User_Id_With_Loans_Inactive_User()
    {
        //Arrange

        //Act
        var result = await _userController.GetUserByIdLoans(4);

        //Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task Should_Return_BadRequest_Whend_Put_User_With_Unregistred_Id()
    {
        //Arrange
        var updateUserCommand = new UpdateUserCommand
        {
            UserId = 10,
            Name = "Rafael Oliveira",
            Email = "rafael@gmail.com",
            BirthDate = new DateTime(1987, 7, 3),
            Password = "1Q2w3e4R#!"
        };

        //Act
        var result = await _userController.Put(updateUserCommand);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Should_Return_BadRequest_Whend_Put_User_With_Inactive_User()
    {
        //Arrange
        var updateUserCommand = new UpdateUserCommand
        {
            UserId = 4,
            Name = "Rafael Oliveira",
            Email = "rafael@gmail.com",
            BirthDate = new DateTime(1987, 7, 3),
            Password = "1Q2w3e4R#!"
        };

        //Act
        var result = await _userController.Put(updateUserCommand);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Should_Return_BadRequest_Whend_Patch_User_With_Null_JsonPatchDocument()
    {
        //Arrange
        JsonPatchDocument<UpdateUserOnlyActiveCommand> patchUser = null;

        //Act
        var result = await _userController.Patch(4, patchUser);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Should_Return_BadRequest_Whend_Patch_User_With_Invalid_Id()
    {
        //Arrange
        var patchUser = new JsonPatchDocument<UpdateUserOnlyActiveCommand>();

        //Act
        var result = await _userController.Patch(0, patchUser);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Should_Return_BadRequest_Whend_Patch_User_Propertie_Active_When_Id_Unregistred()
    {
        //Arrange
        var patchUser = new JsonPatchDocument<UpdateUserOnlyActiveCommand>();

        //Act
        var result = await _userController.Patch(10, patchUser);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Should_Return_BadRequest_Whend_Patch_User_Propertie_Active_When_Already_Active()
    {
        //Arrange
        var patchUser = new JsonPatchDocument<UpdateUserOnlyActiveCommand>();

        //Act
        var result = await _userController.Patch(1, patchUser);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Should_Return_BadRequest_Whend_Delete_User_When_Id_Unregistred()
    {
        //Arrange

        //Act
        var result = await _userController.Delete(10);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Should_Return_BadRequest_Whend_Delete_User_When_User_Inactive()
    {
        //Arrange

        //Act
        var result = await _userController.Delete(4);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Should_Return_BadRequest_Whend_Delete_User_When_User_Has_Loans_Unreturned()
    {
        //Arrange

        //Act
        var result = await _userController.Delete(1);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Should_Return_Ok_Whend_Get_User_Search_Criteria_Met()
    {
        //Arrange

        //Act
        var result = await _userController.Get("silva");

        //Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Should_Return_NoContent_Whend_Put_User()
    {
        //Arrange
        var updateUserCommand = new UpdateUserCommand
        {
            UserId = 1,
            Name = "Rafael Oliveira",
            Email = "rafael@gmail.com",
            BirthDate = new DateTime(1987, 7, 3),
            Password = "1Q2w3e4R#!"
        };

        //Act
        var result = await _userController.Put(updateUserCommand);

        //Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Should_Return_NoContent_Whend_Patch_User_Propertie_Active()
    {
        //Arrange
        var patchUser = new JsonPatchDocument<UpdateUserOnlyActiveCommand>();

        //Act
        var result = await _userController.Patch(4, patchUser);

        //Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Should_Return_NoContent_Whend_Delete_User_Inativating_It()
    {
        //Arrange        

        //Act
        var result = await _userController.Delete(5);

        //Assert
        Assert.IsType<NoContentResult>(result);
    }

}
