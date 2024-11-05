using BookManagement.Application.Commands.CreateBook;
using BookManagement.Application.Commands.UpdateBook;
using BookManagement.Application.Commands.UpdateBookOnlyActive;
using BookManagement.Application.Commands.UpdateBookStock;
using BookManagement.Application.Models;

namespace BookManagement.WebApp.Services;

public interface IBookService
{
    Task<ResultViewModel<IEnumerable<BookViewModel>>> GetAllBooks();
    Task<ResultViewModel<BookDetailViewModel>> GetBookById(int id);
    Task<ResultViewModel<BookLoansViewModel>> GetBookByIdLoans(int id);
    Task<ResultViewModel<int>> CreateBook(CreateBookCommand command);
    Task<ResultViewModel> UpdateBookStock(UpdateBookStockCommand command);
    Task<ResultViewModel> UpdateBook(UpdateBookCommand command);
    Task<ResultViewModel> UpdateBookOnlyActive(int id, UpdateBookOnlyActiveCommand command);
    Task<ResultViewModel> DeleteBook(int id);
}
