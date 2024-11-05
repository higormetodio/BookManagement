
using BookManagement.Application.Models;
using BookManagement.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookManagement.WebApp.Pages.Loans
{
    [Authorize]
    public class ReturnLoanModel : PageModel
    {
        private readonly ILoanService _loanService;

        public ReturnLoanModel(ILoanService loanService)
        {
            _loanService = loanService;
        }

        public LoanDetailViewModel Loan { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var result = await _loanService.GetLoanById(id);

            if (!result.IsSuccess)
            {
                TempData["mensage"] = result.Message;
                TempData["error"] = true;

                return RedirectToPage("/Loans/LoanDetails", new { Id = id});
            }

            Loan = result.Data;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var result = await _loanService.ReturnLoan(id);

            if (!result.IsSuccess)
            {
                TempData["mensage"] = result.Message;
                TempData["error"] = true;

                return RedirectToPage("/Loans/LoanDetails", new { Id = id });
            }

            TempData["mensage"] = $"Empréstimo com ID {id} devolvido com sucesso.";
            TempData["error"] = false;

            return RedirectToPage("/Loans/Index");
        }
    }
}
