using BookManagement.Application.Commands.UpdateBook;
using BookManagement.Application.Models;
using BookManagement.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookManagement.WebApp.Pages.Books
{
    [Authorize]
    public class BookLoansModel : PageModel
    {
        private readonly IBookService _bookService;

        public BookLoansModel(IBookService bookService)
        {
            _bookService = bookService;
        }

        public BookLoansViewModel Book { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var result = await _bookService.GetBookByIdLoans(id);

            if (!result.IsSuccess)
            {
                TempData["mensage"] = result.Message;
                TempData["error"] = true;

                return RedirectToPage("/Books/Index");
            }

            Book = result.Data;

            return Page();

        }
    }
}
