using BookManagement.Application.Commands.CreateUser;
using BookManagement.Application.Commands.DeleteUser;
using BookManagement.Application.Commands.UpdateUser;
using BookManagement.Application.Commands.UpdateUserOnlyActive;
using BookManagement.Application.Queries.GetAllUsers;
using BookManagement.Application.Queries.GetUserById;
using BookManagement.Application.Queries.GetUserLoans;
using BookManagement.Core.Repositories;
using BookManagement.Core.Services;
using BookManagement.UnitTests.Infrastructure.FakeAuth;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;
using BookManagement.UnitTests.Infrastructure.FakePersistence;
using MediatR;

namespace BookManagement.UnitTests.API;
public class FakeMediatorUser : IMediator
{
    private readonly FakeData _fakeData;
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;

    public FakeMediatorUser()
    {
        _fakeData = new FakeData();
        _userRepository = new FakeUserRepository();
        _authService = new FakeAuthService();
    }
    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {        
        if (request is GetAllUsersQuery getAllUsersQuery)
        {
            var getAllUsersHandler = new GetAllUsersHandler(_userRepository);
            var resultViewModel = await getAllUsersHandler.Handle(getAllUsersQuery, cancellationToken);

            return await Task.FromResult((TResponse)(object)resultViewModel);
        }

        if (request is GetUserByIdQuery getUserByIdQuery)
        {
            var getUserByIdHandler = new GetUserByIdHandler(_userRepository);
            var resultViewModel = await getUserByIdHandler.Handle(getUserByIdQuery, cancellationToken);

            return await Task.FromResult((TResponse)(object)resultViewModel);
        }

        if (request is GetUserLoansQuery getUserLoansQuery)
        {
            var getUserLoansHandler = new GetUserLoansHandler(_userRepository);
            var resultViewModel = await getUserLoansHandler.Handle(getUserLoansQuery, cancellationToken);

            return await Task.FromResult((TResponse)(object)resultViewModel);
        }

        if (request is CreateUserCommand createUserCommand)
        {
            var createUserHandler = new CreateUserHandler(_userRepository, _authService);
            var resultViewModel = await createUserHandler.Handle(createUserCommand, cancellationToken);

            return await Task.FromResult((TResponse)(object)resultViewModel);
        }

        if (request is UpdateUserCommand updateUserCommand)
        {
            var updateUserHandler = new UpdateUserHandler(_userRepository, _authService);
            var resultViewModel = await updateUserHandler.Handle(updateUserCommand, cancellationToken);

            return await Task.FromResult((TResponse)(object)resultViewModel);
        }

        if (request is UpdateUserOnlyActiveCommand updateUserOnlyActiveCommand)
        {
            var updateUserOnlyActiveHandler = new UpdateUserOnlyActiveHandler(_userRepository);
            var resultViewModel = await updateUserOnlyActiveHandler.Handle(updateUserOnlyActiveCommand, cancellationToken);

            return await Task.FromResult((TResponse)(object)resultViewModel);
        }

        if (request is DeleteUserCommand deleteUserCommand)
        {
            var deleteUserHandler = new DeleteUserHandler(_userRepository);
            var resultViewModel = await deleteUserHandler.Handle(deleteUserCommand, cancellationToken);

            return await Task.FromResult((TResponse)(object)resultViewModel);
        }

        return await Task.FromResult(default(TResponse));
    }
    public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task Publish(object notification, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
    {
        throw new NotImplementedException();
    }

    public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest
    {
        throw new NotImplementedException();
    }

    public Task<object?> Send(object request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
