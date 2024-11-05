using BookManagement.Application.Models;
using BookManagement.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookManagement.WebApp.Pages.Books
{
    [Authorize]
    public class DeleteBookModel : PageModel
    {
        private readonly IBookService _bookService;

        public DeleteBookModel(IBookService bookService)
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

                return RedirectToPage("/Books/BookDetails", new { Id = id});
            }

            Book = result.Data;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {            
            var result = await _bookService.DeleteBook(id);

            if (!result.IsSuccess)
            {
                TempData["mensage"] = result.Message;
                TempData["error"] = true;

                return RedirectToPage("/Books/BookDetails", new { Id = id});
            }

            TempData["mensage"] = $"Livro com ID {id} excluído com sucesso.";
            TempData["error"] = false;

            return RedirectToPage("/Books/Index");
        }
    }
}
