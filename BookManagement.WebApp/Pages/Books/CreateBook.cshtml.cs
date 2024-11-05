using BookManagement.Application.Commands.CreateBook;
using BookManagement.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;


namespace BookManagement.WebApp.Pages.Books
{
    [Authorize]
    public class CreateBookModel : PageModel
    {
        private readonly IBookService _bookService;

        public CreateBookModel(IBookService bookService)
        {
            _bookService = bookService;
        }

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


        public async Task<IActionResult> OnPostAsync()
        {
            var book = new CreateBookCommand
            {
                Title = Title,
                Author = Author,
                ISBN = ISBN,
                PublicationYear = PublicationYear
            };
           var result = await _bookService.CreateBook(book);

            if (!result.IsSuccess)
            {
                TempData["mensage"] = result.Message;
                TempData["error"] = true;

                return Page();
            }

            TempData["mensage"] = "Cadastro realizado com sucesso.";
            TempData["error"] = false;

            return RedirectToPage($"/Books/BookDetails", new {Id = result.Data});
        }
    }
}
