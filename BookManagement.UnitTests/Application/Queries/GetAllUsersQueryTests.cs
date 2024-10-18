using BookManagement.Application.Queries.GetAllUsers;
using BookManagement.Core.Entities;
using BookManagement.Core.Repositories;
using BookManagement.UnitTests.Infrastructure.FakePersistence;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;

namespace BookManagement.UnitTests.Application.Queries;
public class GetAllUsersQueryTests
{
    private readonly FakeData _fakeData;
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryTests()
    {
        _fakeData = new FakeData();
        _userRepository = new FakeUserRepository();
    }

    [Fact]
    public async Task Should_Not_Return_Users_As_UserViewModel_With_Inactive_Status()
    {
        //Arrange
        var getAllUsersQuery = new GetAllUsersQuery("Alexandre");
        var getAllUsersHandler = new GetAllUsersHandler(_userRepository);

        //Act
        var userResultViewModel = await getAllUsersHandler.Handle(getAllUsersQuery, new CancellationToken());

        //Assert
        Assert.NotNull(userResultViewModel);
        Assert.False(userResultViewModel.IsSuccess);
        Assert.Equal("Users with searched criteria, not found.", userResultViewModel.Message);
    }

    [Fact]
    public async Task Should_Not_Return_Users_As_UserViewModel_When_Word_Not_Found()
    {
        //Arrange
        var getAllUsersQuery = new GetAllUsersQuery("Felipe Luis");
        var getAllUsersHandler = new GetAllUsersHandler(_userRepository);

        //Act
        var userResultViewModel = await getAllUsersHandler.Handle(getAllUsersQuery, new CancellationToken());

        //Assert
        Assert.NotNull(userResultViewModel);
        Assert.False(userResultViewModel.IsSuccess);
        Assert.Equal("Users with searched criteria, not found.", userResultViewModel.Message);
    }

    [Fact]
    public async Task Should_Return_Five_Active_Users_As_UserViewModel()
    {
        //Arrange
        var getAllUsersQuery = new GetAllUsersQuery("");
        var getAllUsersHandler = new GetAllUsersHandler(_userRepository);

        //Act
        var userResultViewModel = await getAllUsersHandler.Handle(getAllUsersQuery, new CancellationToken());
        var userViewModelList = userResultViewModel.Data.ToList();

        //Assert
        Assert.NotNull(userResultViewModel);
        Assert.True(userResultViewModel.IsSuccess);
        Assert.NotNull(userViewModelList);
        Assert.NotEmpty(userViewModelList);
        Assert.Equal(5, userViewModelList.Count());
    }

    [Fact]
    public async Task Should_Return_One_Active_User_As_UserViewModel_By_Name()
    {
        //Arrange
        var getAllUsersQuery = new GetAllUsersQuery("Andre");
        var getAllUsersHandler = new GetAllUsersHandler(_userRepository);
        var user = new User("Andre Silva", "lucas@gmail.com", "1Q2w3e4R#!", "user", new DateTime(1979, 10, 7));

        //Act
        var userResultViewModel = await getAllUsersHandler.Handle(getAllUsersQuery, new CancellationToken());
        var userViewModel = userResultViewModel.Data.SingleOrDefault();

        //Assert
        Assert.NotNull(userResultViewModel);
        Assert.True(userResultViewModel.IsSuccess);
        Assert.NotNull(userViewModel);
        Assert.Equal(user.Name, userViewModel.Name);
    }

    [Fact]
    public async Task Should_Return_Two_Active_Users_As_UserViewModel_By_Name()
    {
        //Arrange
        var getAllUsersQuery = new GetAllUsersQuery("silva");
        var getAllUsersHandler = new GetAllUsersHandler(_userRepository);
        var user1 = new User("Andre Silva", "lucas@gmail.com", "1Q2w3e4R#!", "user", new DateTime(1979, 10, 7));
        var user2 = new User("Fernanda Silva", "fernanda@gmail.com", "1Q2w3e4R#!", "user", new DateTime(1990, 7, 2));

        //Act
        var userResultViewModel = await getAllUsersHandler.Handle(getAllUsersQuery, new CancellationToken());
        var userViewModelList = userResultViewModel.Data.ToList();
        var userViewModel1 = userResultViewModel.Data.SingleOrDefault(b => b.Id == 1);
        var userViewModel2 = userResultViewModel.Data.SingleOrDefault(b => b.Id == 3);

        //Assert
        Assert.NotNull(userViewModelList);
        Assert.True(userResultViewModel.IsSuccess);
        Assert.NotEmpty(userViewModelList);
        Assert.Contains(user1.Name, userViewModel1.Name);
        Assert.Contains(user2.Name, userViewModel2.Name);
    }
}
