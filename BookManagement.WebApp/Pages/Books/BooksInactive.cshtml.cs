using BookManagement.Application.Models;
using BookManagement.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookManagement.WebApp.Pages.Books
{
    [Authorize]
    public class BooksInactiveModel : PageModel
    {
        private readonly IBookService _bookService;

        public BooksInactiveModel(IBookService bookService)
        {
            _bookService = bookService;
        }

        public IEnumerable<BookViewModel> Books { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var result = await _bookService.GetAllBooks();

            if (!result.IsSuccess)
            {
                TempData["mensage"] = result.Message;
                TempData["error"] = true;

                return RedirectToPage("/Books/Index");
            }

            var books = result.Data;
            books = books.Where(b => !b.Active);

            Books = books;

            return Page();
        }
    }
}
