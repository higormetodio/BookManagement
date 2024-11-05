using BookManagement.Application.Commands.UpdateBookOnlyActive;
using BookManagement.Application.Models;
using BookManagement.Core.Entities;
using BookManagement.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookManagement.WebApp.Pages.Books
{
    [Authorize]
    public class ActiveBookModel : PageModel
    {
        private readonly IBookService _bookService;

        public ActiveBookModel(IBookService bookService)
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

                return RedirectToPage("/Books/Index");
            }

            Book = result.Data;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var book = new UpdateBookOnlyActiveCommand(id);

            var result = await _bookService.UpdateBookOnlyActive(id, book);

            if (!result.IsSuccess)
            {
                TempData["mensage"] = result.Message;
                TempData["error"] = true;

                return RedirectToPage("/Books/Index");
            }

            TempData["mensage"] = $"Livro com ID {id} foi reativado com sucesso.";
            TempData["error"] = false;

            return RedirectToPage("/Books/BookDetails", new { Id = id });
        }
    }
}
