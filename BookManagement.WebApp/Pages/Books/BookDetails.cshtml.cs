using BookManagement.Application.Models;
using BookManagement.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookManagement.WebApp.Pages.Books
{
    [Authorize]
    public class BookDetailsModel : PageModel
    {
        private readonly IBookService _bookService;

        public BookDetailsModel(IBookService bookService)
        {
            _bookService = bookService;
        }

        public BookDetailViewModel Book { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var result = await _bookService.GetBookById(id);

            if (!result.IsSuccess)
            {
                TempData["mensage"] = result.Message;
                TempData["error"] = true;

                return RedirectToPage("/Books");
            }

            Book = result.Data;

            return Page();
        }
    }
}
