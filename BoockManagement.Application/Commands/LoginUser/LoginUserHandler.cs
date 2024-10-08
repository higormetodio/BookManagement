using BookManagement.Application.Models;
using BookManagement.Core.Repositories;
using BookManagement.Core.Services;
using MediatR;

namespace BookManagement.Application.Commands.LoginUser;
public class LoginUserHandler : IRequestHandler<LoginUserCommand, ResultViewModel<LoginUserViewModel>>
{
    private readonly IAuthService _authService;
    private readonly IUserRepository _userRepository;

    public LoginUserHandler(IAuthService authService, IUserRepository userRepository)
    {
        _authService = authService;
        _userRepository = userRepository;
    }

    public async Task<ResultViewModel<LoginUserViewModel>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = _authService.ComputeSha256Hash(request.Password);

        var user = await _userRepository.GetUserByEmailAndPasswordAsync(request.Email, passwordHash);

        if (user is null)
        {
            return ResultViewModel<LoginUserViewModel>.Error("Enter a valid email or password");
        }

        var token = _authService.GenerateJwtToken(user.Email, user.Role);

        var model = new LoginUserViewModel(user.Email, token);

        return ResultViewModel<LoginUserViewModel>.Success(model);
    }
}
