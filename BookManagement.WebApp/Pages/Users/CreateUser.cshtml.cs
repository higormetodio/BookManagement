using BookManagement.Application.Commands.CreateUser;
using BookManagement.Core.Entities;
using BookManagement.WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookManagement.WebApp.Pages.Users
{
    public class Index1Model : PageModel
    {
        private readonly IUserService _userService;

        public Index1Model(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        [Required(ErrorMessage = "O Nome deve ser preenchido.")]
        public string Name { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "O Email deve ser preenchido.")]
        [EmailAddress(ErrorMessage = "Entre com um e-mail válido.")]
        public string Email { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "A Data de Nascimento deve ser preenchida.")]
        [DataType(DataType.Date, ErrorMessage = "A Data de Nascimento deve ser preenchida.")]
        public DateTime BirthDate { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "A Senha deve ser preenchida.")]
        [DataType(DataType.Password, ErrorMessage = "A Senha deve ser preenchida.")]
        public string Password { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = new CreateUserCommand
            {
                Name = Name,
                Email = Email,
                BirthDate = BirthDate,
                Password = Password,
            };

            var result = await _userService.CreateUser(user);

            if (!result.IsSuccess)
            {
                TempData["mensage"] = result.Message;
                TempData["error"] = true;

                return Page();
            }

            TempData["mensage"] = "Cadastro realizado com sucesso.";
            TempData["error"] = false;

            return RedirectToPage($"/Users/UserDetails", new { Id = result.Data });
        }
    }
}
