using BookManagement.Application.Commands.LoginUser;
using BookManagement.Application.Models;
using BookManagement.WebApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace BookManagement.WebApp.Pages.Users
{
    public class LoginUserModel : PageModel
    {
        private readonly IUserService _userService;

        public LoginUserModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        [Required(ErrorMessage = "O E-mail precisa ser preenchido.")]
        [EmailAddress(ErrorMessage = "Entre com um e-mail válido.")]
        public string? Email { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "A senha precisa ser preenchida.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public bool Remember  { get; set; }
        public string? ReturnUrl { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var command = new LoginUserCommand
            {
                Email = Email,
                Password = Password
            };

            var result = await _userService.LoginUser(command);

            if (!result.IsSuccess)
            {
                TempData["mensage"] = result.Message;
                TempData["error"] = true;

                return Page();
            }

            var loginViewModel = result.Data;

            Response.Cookies.Append("X-Access-Token", loginViewModel.Token);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginViewModel.Email)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToPage("/Index");
        }
    }
}
