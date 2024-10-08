using BookManagement.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BookManagement.Infrastructure.Auth;
public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateJwtToken(string email, string role)
    {
        var issuer = _configuration["JWT:Issuer"];
        var audience = _configuration["JWT:Audience"];

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim("userName", email),
            new Claim(ClaimTypes.Role, role)
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials,
            claims: claims);

        var tokenHandler = new JwtSecurityTokenHandler();

        var stringToken = tokenHandler.WriteToken(token);

        return stringToken;
    }

    public string ComputeSha256Hash(string password)
    {
        using SHA256 sha256Hash = SHA256.Create();

        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

        StringBuilder stringBuilder = new();

        for (int i = 0; i < bytes.Length; i++)
        {
            stringBuilder.Append(bytes[i].ToString("X2"));
        }

        return stringBuilder.ToString();
    }
}
