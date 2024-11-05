using BookManagement.Application.Models;
using BookManagement.Core.Enums;
using BookManagement.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Security;

namespace BookManagement.WebApp.Pages.Loans
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILoanService _loanService;

        public IndexModel(ILoanService loanService)
        {
            _loanService = loanService;
        }

        public IEnumerable<LoanViewModel> Loans { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var result = await _loanService.GetAllLoans();

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("error", result.Message);

                return RedirectToPage("/Users/LoginUser");
            }

            var loans = result.Data;
            loans = loans.Where(l => l.Status != LoanStatus.Returned.ToString());

            Loans = loans;

            return Page();
        }
    }
}
