using BookManagement.Application.Models;
using BookManagement.Core.Enums;
using BookManagement.WebApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace BookManagement.WebApp.Pages;

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
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            return RedirectToPage("/Users/LoginUser");
        }

        var loans = result.Data;

        loans = loans.Where(l => l.Status == LoanStatus.Late.ToString());

        Loans = loans;

        return Page();
    }
}
