using BookManagement.Application.Commands.UpdateBook;
using BookManagement.Application.Commands.UpdateUser;
using BookManagement.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BookManagement.WebApp.Pages.Users
{
    [Authorize]
    public class UpdateUserModel : PageModel
    {
        private readonly IUserService _userService;

        public UpdateUserModel(IUserService userService)
        {
            _userService = userService;
        }

        public int Id { get; set; }
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

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var result = await _userService.GetUserById(id);

            if (!result.IsSuccess)
            {
                TempData["mensage"] = result.Message;
                TempData["error"] = true;

                return RedirectToPage($"/Users/UserDetails", new { Id = id });
            }

            Id = result.Data.Id;
            Name = result.Data.Name;
            Email = result.Data.Email;
            BirthDate = result.Data.BirthDate;
            Password = result.Data.Password;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var updateUserCommand = new UpdateUserCommand
            {
                UserId = id,
                Name = this.Name,
                Email = this.Email,
                BirthDate = this.BirthDate,
                Password = this.Password
            };

            var result = await _userService.UpdateUser(updateUserCommand);

            if (!result.IsSuccess)
            {
                TempData["mensage"] = result.Message;
                TempData["error"] = true;

                return RedirectToPage($"/Users/UserDetails", new { Id = updateUserCommand.UserId});
            }

            TempData["mensage"] = $"Usuário {updateUserCommand.Name} atualizado com sucesso.";
            TempData["error"] = false;

            return RedirectToPage($"/Users/UserDetails", new { Id = updateUserCommand.UserId });
        }
    }
}
