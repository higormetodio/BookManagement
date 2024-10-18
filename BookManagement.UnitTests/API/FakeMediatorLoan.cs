using BookManagement.Application.Commands.CreateLoan;
using BookManagement.Application.Commands.ReturnLoan;
using BookManagement.Application.Queries.GetAllLoans;
using BookManagement.Application.Queries.GetLoanById;
using BookManagement.Core.Repositories;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;
using BookManagement.UnitTests.Infrastructure.FakePersistence;
using MediatR;

namespace BookManagement.UnitTests.API;
public class FakeMediatorLoan : IMediator
{
    private readonly FakeData _fakeData;
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILoanRepository _loanRepository;

    public FakeMediatorLoan()
    {
        _fakeData = new FakeData();
        _bookRepository = new FakeBookRepository();
        _userRepository = new FakeUserRepository();
        _loanRepository = new FakeLoanRepository();
    }
    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        if (request is GetAllLoansQuery getAllLoansQuery)
        {
            var getAllLoansHandler = new GetAllLoansHandler(_loanRepository);
            var resultViewModel = await getAllLoansHandler.Handle(getAllLoansQuery, cancellationToken);

            return await Task.FromResult((TResponse)(object)resultViewModel);
        }

        if (request is GetLoanByIdQuery getLoanByIdQuery)
        {
            var getLoanByIdHandler = new GetLoanByIdHandler(_loanRepository);
            var resultViewModel = await getLoanByIdHandler.Handle(getLoanByIdQuery, cancellationToken);

            return await Task.FromResult((TResponse)(object)resultViewModel);
        }

        if (request is CreateLoanCommand createLoanCommand)
        {
            var createLoanHandler = new CreateLoanHandler(_loanRepository, _bookRepository, _userRepository);
            var resultViewModel = await createLoanHandler.Handle(createLoanCommand, cancellationToken);

            return await Task.FromResult((TResponse)(object)resultViewModel);
        }

        if (request is ReturnLoanCommand returnLoanCommand)
        {
            var returnLoanHadler = new ReturnLoanHandler(_loanRepository, _bookRepository);
            var resultViewModel = await returnLoanHadler.Handle(returnLoanCommand, cancellationToken);

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
