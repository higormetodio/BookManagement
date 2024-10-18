using BookManagement.Application.Queries.GetUserById;
using BookManagement.Core.Repositories;
using BookManagement.UnitTests.Infrastructure.FakePersistence;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;

namespace BookManagement.UnitTests.Application.Queries;
public class GetUserByIdQueryTests
{
    private readonly FakeData _fakeData;
    private readonly IUserRepository _userRepository;
    public GetUserByIdQueryTests()
    {
        _fakeData = new FakeData();
        _userRepository = new FakeUserRepository();
    }

    [Fact]
    public async Task Should_Not_Return_One_User_As_UserDetailViewModel_Given_Unregistred_Id()
    {
        //Arrange
        var getUserByIdQuery = new GetUserByIdQuery(10);
        var getUserByIdHandler = new GetUserByIdHandler(_userRepository);

        //Act
        var userResultViewModel = await getUserByIdHandler.Handle(getUserByIdQuery, new CancellationToken());

        //Assert
        Assert.NotNull(userResultViewModel);
        Assert.False(userResultViewModel.IsSuccess);
        Assert.Equal("User not found.", userResultViewModel.Message);
    }

    [Fact]
    public async Task Should_Not_Return_User_As_UserDetailViewModel_With_Inactive_Status_Given_Id()
    {
        //Arrange
        var getUserByIdQuery = new GetUserByIdQuery(4);
        var getUserByIdHandler = new GetUserByIdHandler(_userRepository);

        //Act
        var userResultViewModel = await getUserByIdHandler.Handle(getUserByIdQuery, new CancellationToken());

        //Assert
        Assert.NotNull(userResultViewModel);
        Assert.False(userResultViewModel.IsSuccess);
        Assert.Equal("User not found.", userResultViewModel.Message);
    }

    [Fact]
    public async Task Should_Return_One_User_As_UserViewModel_Given_Id()
    {
        //Arrange
        var getUserByIdQuery = new GetUserByIdQuery(1);
        var getUserByIdHandler = new GetUserByIdHandler(_userRepository);
        var user = await _userRepository.GetUserByIdAsync(1);

        //Act
        var userResultViewModel = await getUserByIdHandler.Handle(getUserByIdQuery, new CancellationToken());
        var userDetailViewModel = userResultViewModel.Data;

        //Assert
        Assert.NotNull(userResultViewModel);
        Assert.True(userResultViewModel.IsSuccess);
        Assert.NotNull(userDetailViewModel);
        Assert.Equal(user.Name, userDetailViewModel.Name);
    }
}
