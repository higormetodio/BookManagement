using BookManagement.Application.Models;
using BookManagement.Core.Entities;
using BookManagement.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookManagement.WebApp.Pages.Users
{
    [Authorize]
    public class UserLoansModel : PageModel
    {
        private readonly IUserService _userService;

        public UserLoansModel(IUserService userService)
        {
            _userService = userService;
        }

        public UserLoansViewModel User { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var result = await _userService.GetUserByIdLoans(id);

            if (!result.IsSuccess)
            {
                TempData["mensage"] = result.Message;
                TempData["error"] = true;

                return RedirectToPage("/Users/Index");
            }

            User = result.Data;

            return Page();
        }
    }
}
