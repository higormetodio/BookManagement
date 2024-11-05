using BookManagement.Application.Commands.UpdateUser;
using BookManagement.Core.Repositories;
using BookManagement.Core.Services;
using BookManagement.UnitTests.Infrastructure.FakeAuth;
using BookManagement.UnitTests.Infrastructure.FakePersistence;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;

namespace BookManagement.UnitTests.Application.Commands;
public class UpdateUserCommandTests
{
    private readonly FakeData _fakeData;
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;

    public UpdateUserCommandTests()
    {
        _fakeData = new FakeData();
        _userRepository = new FakeUserRepository();
        _authService = new FakeAuthService();
    }

    [Fact]
    public async Task Should_Not_Update_User_Properties_Given_UpdateUserCommand_With_Unregistred_Id()
    {
        //Arrange
        var userUpdateUserCommand = new UpdateUserCommand
        {
            UserId = 10,
            Name = "Rafael Oliveira",
            Email = "rafael@gmail.com",
            BirthDate = new DateTime(1987, 7, 3),
            Password = "1Q2w3e4R#!"
        };

        var userUpdateUserHandler = new UpdateUserHandler(_userRepository, _authService);

        //Act
        var resultViewModel = await userUpdateUserHandler.Handle(userUpdateUserCommand, new CancellationToken());

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.False(resultViewModel.IsSuccess);
        Assert.Equal("User not found", resultViewModel.Message);
    }

    [Fact]
    public async Task Should_Not_Update_User_Properties_Given_UpdateUserCommand_With_Inactive_Id()
    {
        //Arrange
        var userUpdateUserCommand = new UpdateUserCommand
        {
            UserId = 4,
            Name = "Rafael Oliveira",
            Email = "rafael@gmail.com",
            BirthDate = new DateTime(1987, 7, 3),
            Password = "1Q2w3e4R#!"
        };

        var userUpdateUserHandler = new UpdateUserHandler(_userRepository, _authService);

        //Act
        var resultViewModel = await userUpdateUserHandler.Handle(userUpdateUserCommand, new CancellationToken());

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.False(resultViewModel.IsSuccess);
        Assert.Equal("User not found", resultViewModel.Message);
    }

    [Fact]
    public async Task Should_Update_User_Properties_Given_UpdateUserCommand()
    {
        //Arrange
        var userUpdateUserCommand = new UpdateUserCommand
        {
            UserId = 1,
            Name = "Rafael Oliveira",
            Email = "rafael@gmail.com",
            BirthDate = new DateTime(1987, 7, 3),
            Password = "1Q2w3e4R#!"
        };

        var userUpdateUserHandler = new UpdateUserHandler(_userRepository, _authService);

        //Act
        var resultViewModel = await userUpdateUserHandler.Handle(userUpdateUserCommand, new CancellationToken());
        var user = await _userRepository.GetUserByIdAsync(1);

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.True(resultViewModel.IsSuccess);
        Assert.NotNull(user);
        Assert.Equal(userUpdateUserCommand.UserId, user.Id);
        Assert.Equal(userUpdateUserCommand.Name, user.Name);
        Assert.Equal(userUpdateUserCommand.Email, user.Email);
        Assert.Equal(userUpdateUserCommand.BirthDate, user.BirthDate);
        Assert.Equal(_authService.ComputeSha256Hash(userUpdateUserCommand.Password), user.Password);
    }
}
