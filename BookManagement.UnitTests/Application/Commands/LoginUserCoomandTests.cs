using BookManagement.Application.Commands.CreateUser;
using BookManagement.Application.Commands.LoginUser;
using BookManagement.Core.Repositories;
using BookManagement.Core.Services;
using BookManagement.UnitTests.Infrastructure.FakeAuth;
using BookManagement.UnitTests.Infrastructure.FakePersistence;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;

namespace BookManagement.UnitTests.Application.Commands;
public class LoginUserCoomandTests
{
    private readonly FakeData _fakeData;
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;

    public LoginUserCoomandTests()
    {
        _fakeData = new FakeData();
        _userRepository = new FakeUserRepository();
        _authService = new FakeAuthService();
    }

    [Fact]
    public async Task Should_Not_Return_LoginViewModel_After_Incorrect_Login_User_Password()
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
        await userCreateUserHandler.Handle(userCreateUserCommand, new CancellationToken());

        var userLoginUserCommand = new LoginUserCommand
        {
            Email = "rafael@gmail.com",
            Password = "1Q2w3e4R&!"
        };

        var userLoginUserHandler = new LoginUserHandler(_authService, _userRepository);

        //Act
        var resultViewModel = await userLoginUserHandler.Handle(userLoginUserCommand, new CancellationToken());
        var loginViewModel = resultViewModel.Data;

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.False(resultViewModel.IsSuccess);
        Assert.Equal("Enter a valid email or password", resultViewModel.Message);
    }

    [Fact]
    public async Task Should_Not_Return_LoginViewModel_After_Incorrect_Login_User_Email()
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
        await userCreateUserHandler.Handle(userCreateUserCommand, new CancellationToken());

        var userLoginUserCommand = new LoginUserCommand
        {
            Email = "rafael@gmail.com.br",
            Password = "1Q2w3e4R#!"
        };

        var userLoginUserHandler = new LoginUserHandler(_authService, _userRepository);

        //Act
        var resultViewModel = await userLoginUserHandler.Handle(userLoginUserCommand, new CancellationToken());
        var loginViewModel = resultViewModel.Data;

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.False(resultViewModel.IsSuccess);
        Assert.Equal("Enter a valid email or password", resultViewModel.Message);
    }

    [Fact]
    public async Task Should_Return_LoginViewModel_After_Correct_Login_User()
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
        await userCreateUserHandler.Handle(userCreateUserCommand, new CancellationToken());

        var userLoginUserCommand = new LoginUserCommand
        {
            Email = "rafael@gmail.com",
            Password = "1Q2w3e4R#!"
        };

        var userLoginUserHandler = new LoginUserHandler(_authService, _userRepository);

        //Act
        var resultViewModel = await userLoginUserHandler.Handle(userLoginUserCommand, new CancellationToken());
        var loginViewModel = resultViewModel.Data;

        //Assert
        Assert.NotNull(resultViewModel);
        Assert.True(resultViewModel.IsSuccess);
        Assert.NotNull(loginViewModel);
        Assert.Equal(userLoginUserCommand.Email, loginViewModel.Email);
        Assert.Equal(_authService.GenerateJwtToken(userCreateUserCommand.Email, userCreateUserCommand.Role), loginViewModel.Token);
    }
}
