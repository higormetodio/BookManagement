using BookManagement.Application.Models;
using BookManagement.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookManagement.WebApp.Pages.Users
{
    [Authorize]
    public class UsersInavtiveModel : PageModel
    {
        private readonly IUserService _userService;

        public UsersInavtiveModel(IUserService userService)
        {
            _userService = userService;
        }

        public IEnumerable<UserViewModel> Users { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var result = await _userService.GetAllUsers();

            if (!result.IsSuccess)
            {
                TempData["mensage"] = result.Message;
                TempData["error"] = true;

                return RedirectToPage("/Users/Index");
            }

            var users = result.Data;
            users = users.Where(b => !b.Active);

            Users = users;

            return Page();
        }
    }
}
