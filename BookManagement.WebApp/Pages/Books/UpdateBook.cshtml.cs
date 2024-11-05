using BookManagement.Application.Commands.UpdateBook;
using BookManagement.Application.Commands.UpdateBookStock;
using BookManagement.Application.Models;
using BookManagement.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BookManagement.WebApp.Pages.Books
{
    [Authorize]
    public class UpdateBookModel : PageModel
    {
        private readonly IBookService _bookService;

        public UpdateBookModel(IBookService bookService)
        {
            _bookService = bookService;
        }

        public int Id { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "O Título deve ser preenchido.")]
        public string Title { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "O Autor deve ser preenchido.")]
        public string Author { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "O ISBN deve ser preenchido.")]
        public string ISBN { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "O Título Ano de Publicação deve ser preenchido.")]
        public int PublicationYear { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "A Quantidade deve ser preenchido.")]
        public int Quantity { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var result = await _bookService.GetBookById(id);

            if (!result.IsSuccess)
            {
                TempData["mensage"] = result.Message;
                TempData["error"] = true;

                return RedirectToPage($"/Books/BookDetails", new { Id = id });
            }

            Id = result.Data.Id;
            Title = result.Data.Title;
            Author = result.Data.Author;
            ISBN = result.Data.ISBN;
            PublicationYear = result.Data.PublicationYear;
            Quantity = result.Data.Quantity;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var updateBookCommand = new UpdateBookCommand
            {
                BookId = id,
                Title = this.Title,
                Author = this.Author,
                ISBN = this.ISBN,
                PublicationYear = this.PublicationYear
            };

            var updateBookStockCommand = new UpdateBookStockCommand
            {
                BookId = id,
                Quantity = this.Quantity
            };

            var result = await _bookService.UpdateBook(updateBookCommand);

            var resultStock = await _bookService.UpdateBookStock(updateBookStockCommand);

            if (!result.IsSuccess)
            {
                TempData["mensage"] = result.Message;
                TempData["error"] = true;

                return RedirectToPage($"/Books/BookDetails", new { Id = updateBookCommand.BookId });
            }

            if (!resultStock.IsSuccess)
            {
                TempData["mensage"] = resultStock.Message;
                TempData["error"] = true;

                return RedirectToPage($"/Books/BookDetails", new { Id = updateBookCommand.BookId });
            }

            TempData["mensage"] = $"Livro \"{updateBookCommand.Title}\" atualizado com sucesso.";
            TempData["error"] = false;

            return RedirectToPage($"/Books/BookDetails", new { Id = updateBookCommand.BookId });
        }
    }
}
