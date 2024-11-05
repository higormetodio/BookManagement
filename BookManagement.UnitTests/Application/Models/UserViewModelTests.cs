using BookManagement.Application.Models;
using BookManagement.Core.Repositories;
using BookManagement.UnitTests.Infrastructure.FakePersistence;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;

namespace BookManagement.UnitTests.Application.Models;
public class UserViewModelTests
{
    private readonly FakeData _fakeData;
    private readonly IUserRepository _userRepository;

    public UserViewModelTests()
    {
        _fakeData = new FakeData();
        _userRepository = new FakeUserRepository();
    }

    [Fact]
    public async Task Should_Create_User_View_Model_With_Correct_Properties()
    {
        //Arrange
        var user = await _userRepository.GetUserByIdAsync(1);

        //Act
        var userViewModel = new UserViewModel(user.Id, user.Name, user.Email, user.BirthDate, user.Password, user.Active);

        //Assert
        Assert.NotNull(userViewModel);
        Assert.Equal(user.Id, userViewModel.Id);
        Assert.Equal(user.Name, userViewModel.Name);
        Assert.Equal(user.Email, userViewModel.Email);
        Assert.Equal(user.BirthDate, userViewModel.BirthDate);
        Assert.Equal(user.Password, userViewModel.Password);
        Assert.Equal(user.Active, userViewModel.Active);

    }
}
