using BookManagement.Application.Commands.DeleteUser;
using BookManagement.Core.Repositories;
using BookManagement.UnitTests.Infrastructure.FakePersistence;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;

namespace BookManagement.UnitTests.Application.Commands;
public class DeleteUserCoomandTests
{
    private readonly FakeData _fakeData;
    private readonly IUserRepository _userRepository;

    public DeleteUserCoomandTests()
    {
        _fakeData = new FakeData();
        _userRepository = new FakeUserRepository();
    }

    [Fact]
    public async Task Should_Not_Inactivate_User_As_DeleteUserCommand_Give_Unregistred_Id()
    {
        //Arrange
        var userDeleteUserCommand = new DeleteUserCommand(10);
        var userDeleteUserHandler = new DeleteUserHandler(_userRepository);

        //Act
        var resultViewModel = await userDeleteUserHandler.Handle(userDeleteUserCommand, new CancellationToken());

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.False(resultViewModel.IsSuccess);
        Assert.Equal("User not found.", resultViewModel.Message);
    }

    [Fact]
    public async Task Should_Not_Inactivate_User_As_DeleteUserCommand_Given_Inactive_User()
    {
        //Arrange
        var userDeleteUserCommand = new DeleteUserCommand(4);
        var userDeleteUserHandler = new DeleteUserHandler(_userRepository);

        //Act
        var resultViewModel = await userDeleteUserHandler.Handle(userDeleteUserCommand, new CancellationToken());

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.False(resultViewModel.IsSuccess);
        Assert.Equal("User not found.", resultViewModel.Message);
    }

    [Fact]
    public async Task Should_Not_Inactivate_User_As_DeleteUserCommand_With_LoanStatus_As_Loaned_Or_Late()
    {
        //Arrange
        var userDeleteUserCommand = new DeleteUserCommand(1);
        var userDeleteUserHandler = new DeleteUserHandler(_userRepository);

        //Act
        var resultViewModel = await userDeleteUserHandler.Handle(userDeleteUserCommand, new CancellationToken());

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.False(resultViewModel.IsSuccess);
        Assert.Equal("The user cannot be deleted as he has active loans.", resultViewModel.Message);
    }

    [Fact]
    public async Task Should_Inactivate_User_As_DeleteUserCommand_Give_Id()
    {
        //Arrange
        var userDeleteUserCommand = new DeleteUserCommand(5);
        var userDeleteUserHandler = new DeleteUserHandler(_userRepository);

        //Act
        var resultViewModel = await userDeleteUserHandler.Handle(userDeleteUserCommand, new CancellationToken());
        var userAfterDelete = await _userRepository.GetUserByIdAsync(5);

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.True(resultViewModel.IsSuccess);
        Assert.False(userAfterDelete.Active);
    }
}
