using BookManagement.Application.Commands.CreateLoan;
using BookManagement.Application.Models;
using BookManagement.Core.Entities;
using BookManagement.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BookManagement.WebApp.Pages.Loans
{
    [Authorize]
    public class CreateLoanModel : PageModel
    {
        private readonly ILoanService _loanService;
        private readonly IUserService _userService;
        private readonly IBookService _bookService;

        public CreateLoanModel(ILoanService loanService, IUserService userService, IBookService bookService)
        {
            _loanService = loanService;
            _userService = userService;
            _bookService = bookService;
        }

        public List<UserViewModel> Users { get; set; }        
        public List<BookViewModel> Books { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "O Usuário deve ser selecionado.")]
        public int UserId { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "O Livro deve ser selecionado.")]
        public int BookId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var resultUser = await _userService.GetAllUsers();
            var resultBook = await _bookService.GetAllBooks();

            Users = resultUser.Data.Where(u => u.Active).OrderBy(u => u.Name).ToList();
            Books = resultBook.Data.Where(b => b.Active).OrderBy(b => b.Title).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var loan = new CreateLoanCommand
            {
                UserId = this.UserId,
                BookId = this.BookId
            };

            var result = await _loanService.CreateLoan(loan);

            if (!result.IsSuccess)
            {
                TempData["mensage"] = result.Message;
                TempData["error"] = true;

                return RedirectToPage("/Loans/Index");
            }

            TempData["mensage"] = "Empréstimo criado com sucesso.";
            TempData["error"] = false;

            return RedirectToPage($"/Loans/LoanDetails", new { Id = result.Data });
        }
    }
}
