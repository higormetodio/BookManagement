using BookManagement.Application.Commands.UpdateUserOnlyActive;
using BookManagement.Core.Repositories;
using BookManagement.UnitTests.Infrastructure.FakePersistence;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;

namespace BookManagement.UnitTests.Application.Commands;
public class UpdateUserOnlyActiveCommandTests
{
    private readonly FakeData _fakeData;
    private readonly IUserRepository _userRepository;

    public UpdateUserOnlyActiveCommandTests()
    {
        _fakeData = new FakeData();
        _userRepository = new FakeUserRepository();
    }

    [Fact]
    public async Task Should_Not_Update_User_Status_Property_Given_UpdateUserOnlyActiveCommand_With_Unregistred_Id()
    {
        //Arrange
        var userUpdateUserOnlyActiveCommand = new UpdateUserOnlyActiveCommand(10)
        {
            Active = true
        };

        var userUpdateUserOnlyActiveHandler = new UpdateUserOnlyActiveHandler(_userRepository);

        //Act
        var resultViewModel = await userUpdateUserOnlyActiveHandler.Handle(userUpdateUserOnlyActiveCommand, new CancellationToken());
        var user = await _userRepository.GetUserByIdAsync(4);

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.False(resultViewModel.IsSuccess);
        Assert.Equal("User not found.", resultViewModel.Message);
    }

    [Fact]
    public async Task Should_Not_Update_User_Status_Property_Given_UpdateUserOnlyActiveCommand_With_User_Already_Active()
    {
        //Arrange
        var userUpdateUserOnlyActiveCommand = new UpdateUserOnlyActiveCommand(1)
        {
            Active = true
        };

        var userUpdateUserOnlyActiveHandler = new UpdateUserOnlyActiveHandler(_userRepository);

        //Act
        var resultViewModel = await userUpdateUserOnlyActiveHandler.Handle(userUpdateUserOnlyActiveCommand, new CancellationToken());
        var user = await _userRepository.GetUserByIdAsync(4);

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.False(resultViewModel.IsSuccess);
        Assert.Equal("User is already active.", resultViewModel.Message);
    }

    [Fact]
    public async Task Should_Update_User_Status_Property_Given_UpdateUserOnlyActiveCommand()
    {
        //Arrange
        var userUpdateUserOnlyActiveCommand = new UpdateUserOnlyActiveCommand(4)
        {
            Active = true
        };

        var userUpdateUserOnlyActiveHandler = new UpdateUserOnlyActiveHandler(_userRepository);

        //Act
        var resultViewModel = await userUpdateUserOnlyActiveHandler.Handle(userUpdateUserOnlyActiveCommand, new CancellationToken());
        var user = await _userRepository.GetUserByIdAsync(4);

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.True(resultViewModel.IsSuccess);
        Assert.NotNull(user);
        Assert.Equal(userUpdateUserOnlyActiveCommand.Active, user.Active);
        Assert.True(user.Active);
    }
}
