using BookManagement.Application.Commands.CreateUser;
using BookManagement.Core.Repositories;
using BookManagement.Core.Services;
using BookManagement.UnitTests.Infrastructure.FakeAuth;
using BookManagement.UnitTests.Infrastructure.FakePersistence;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BookManagement.UnitTests.Application.Commands;
public class CreateUserCoomandTestes
{
    private readonly FakeData _fakeData;
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;

    public CreateUserCoomandTestes()
    {
        _fakeData = new FakeData();
        _userRepository = new FakeUserRepository();
        _authService = new FakeAuthService();
    }

    [Fact]
    public async Task Shoud_Create_User_As_CreateUserCommand_Returning_Id()
    {
        //Arrange
        var userCreateUserCommand = new CreateUserCommand
        {
            Name = "Rafael Oliveira",
            Email = "rafael@gmail.com",
            BirthDate = new DateTime(1987, 7, 3),
            Password = "1Q2w3e4R#!",
            Role = "user"
        };

        var userCreateUserHandler = new CreateUserHandler(_userRepository, _authService);

        //Act
        var resultViewModel = await userCreateUserHandler.Handle(userCreateUserCommand, new CancellationToken());
        var id = resultViewModel.Data;
        var user = await _userRepository.GetUserByIdAsync(id);

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.True(resultViewModel.IsSuccess);
        Assert.True(id > 0);
        Assert.Equal(userCreateUserCommand.Name, user.Name);
        Assert.Equal(userCreateUserCommand.Email, user.Email);
        Assert.Equal(userCreateUserCommand.BirthDate, user.BirthDate);
        Assert.Equal(_authService.ComputeSha256Hash(userCreateUserCommand.Password), user.Password);
        Assert.Equal(userCreateUserCommand.Role, user.Role);
    }
}
