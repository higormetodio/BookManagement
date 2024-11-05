using BookManagement.Application.Models;
using BookManagement.Core.Entities;
using BookManagement.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookManagement.WebApp.Pages.Users
{
    [Authorize]
    public class DeleteUserModel : PageModel
    {
        private readonly IUserService _userService;

        public DeleteUserModel(IUserService userService)
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

                return RedirectToPage($"/Users/UserDetails", new { Id = id });
            }

            User = result.Data;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var result = await _userService.DeleteUser(id);

            if (!result.IsSuccess)
            {
                TempData["mensage"] = result.Message;
                TempData["error"] = true;

                return RedirectToPage($"/Users/UserDetails", new { Id = id });
            }

            TempData["mensage"] = $"Usuário com ID {id} excluído com sucesso.";
            TempData["error"] = false;

            return RedirectToPage("/Users/Index");
        }
    }
}
