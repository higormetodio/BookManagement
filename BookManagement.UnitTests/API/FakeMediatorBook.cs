using BookManagement.Application.Commands.CreateBook;
using BookManagement.Application.Commands.DeleteBook;
using BookManagement.Application.Commands.UpdateBook;
using BookManagement.Application.Commands.UpdateBookOnlyActive;
using BookManagement.Application.Commands.UpdateBookStock;
using BookManagement.Application.Queries.GetAllBooks;
using BookManagement.Application.Queries.GetBookById;
using BookManagement.Application.Queries.GetBookLoans;
using BookManagement.Core.Repositories;
using BookManagement.UnitTests.Infrastructure.FakePersistence;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;
using MediatR;

namespace BookManagement.UnitTests.API;
public class FakeMediatorBook : IMediator
{
    private readonly FakeData _fakeData;
    private readonly IBookRepository _bookRepository;

    public FakeMediatorBook()
    {
        _fakeData = new FakeData();
        _bookRepository = new FakeBookRepository();
    }
    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        if (request is GetAllBooksQuery getAllBooksQuery)
        {
            var getAllBooksHandler = new GetAllBooksHandler(_bookRepository);
            var resultViewModel = await getAllBooksHandler.Handle(getAllBooksQuery, cancellationToken);

            return await Task.FromResult((TResponse)(object)resultViewModel);
        }

        if (request is GetBookByIdQuery getBookByIdQuery)
        {
            var getBookByIdHandler = new GetBookByIdHandler(_bookRepository);
            var resultViewModel = await getBookByIdHandler.Handle(getBookByIdQuery, cancellationToken);

            return await Task.FromResult((TResponse)(object)resultViewModel);
        }

        if (request is GetBookLoansQuery getBookLoansQuery)
        {
            var getBookLoansHandler = new GetBookLoansHandler(_bookRepository);
            var resultViewModel = await getBookLoansHandler.Handle(getBookLoansQuery, cancellationToken);

            return await Task.FromResult((TResponse)(object)resultViewModel);
        }

        if (request is CreateBookCommand createBookCommand)
        {
            var createBookHandler = new CreateBookHandler(_bookRepository);
            var resultViewModel = await createBookHandler.Handle(createBookCommand, cancellationToken);

            return await Task.FromResult((TResponse)(object)resultViewModel);
        }

        if (request is UpdateBookCommand updateBookCommand)
        {
            var updateBookHandler = new UpdateBookHandler(_bookRepository);
            var resultViewModel = await updateBookHandler.Handle(updateBookCommand, cancellationToken);

            return await Task.FromResult((TResponse)(object)resultViewModel);
        }

        if (request is UpdateBookStockCommand updateBookStockCommand)
        {
            var updateBookStockHandler = new UpdateBookStockHandler(_bookRepository);
            var resultViewModel = await updateBookStockHandler.Handle(updateBookStockCommand, cancellationToken);

            return await Task.FromResult((TResponse)(object)resultViewModel);
        }

        if (request is UpdateBookOnlyActiveCommand updateBookOnlyActiveCommand)
        {
            var updateBookOnlyActiveHandler = new UpdateBookOnlyActiveHandler(_bookRepository);
            var resultViewModel = await updateBookOnlyActiveHandler.Handle(updateBookOnlyActiveCommand, cancellationToken);

            return await Task.FromResult((TResponse)(object)resultViewModel);
        }

        if (request is DeleteBookCommand deleteBookCommand)
        {
            var deleteBookHandler = new DeleteBookHandler(_bookRepository);
            var resultViewModel = await deleteBookHandler.Handle(deleteBookCommand, cancellationToken);

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
