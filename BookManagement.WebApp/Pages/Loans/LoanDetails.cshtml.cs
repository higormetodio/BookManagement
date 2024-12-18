using BookManagement.Application.Models;
using BookManagement.Core.Entities;
using BookManagement.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookManagement.WebApp.Pages.Loans
{
    [Authorize]
    public class LoanDetailsModel : PageModel
    {
        private readonly ILoanService _loanService;

        public LoanDetailsModel(ILoanService loanService)
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

                return RedirectToPage("/Loans");
            }

            Loan = result.Data;

            return Page();
        }
    }
}
