using BookManagement.Application.Commands.UpdateUserOnlyActive;
using BookManagement.Application.Models;
using BookManagement.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookManagement.WebApp.Pages.Users
{
    [Authorize]
    public class ActiveUserModel : PageModel
    {
        private readonly IUserService _userService;

        public ActiveUserModel(IUserService userService)
        {
            _userService = userService;
        }

        public UserViewModel User { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var result = await _userService.GetUserById(id);

            if (!result.IsSuccess)
            {
                TempData["mensage"] = result.Message;
                TempData["error"] = true;

                return RedirectToPage($"/Users/Index");
            }

            User = result.Data;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var user = new UpdateUserOnlyActiveCommand(id);

            var result = await _userService.UpdateUserOnlyActive(id, user);

            if (!result.IsSuccess)
            {
                TempData["mensage"] = result.Message;
                TempData["error"] = true;

                return RedirectToPage($"/Users/Index");
            }

            TempData["mensage"] = $"Usuário com ID {id} foi reativado com sucesso.";
            TempData["error"] = false;

            return RedirectToPage("/Users/UserDetails", new { Id = id });
        }
    }
}
