using BookManagement.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BookManagement.UnitTests.Infrastructure.FakeAuth;
public class FakeAuthService : IAuthService
{
    public string GenerateJwtToken(string email, string role)
    {
        var issuer = "AuthServiceTesteIssuer";
        var audience = "AuthServiceTesteAudience";

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("9V7JX48Vu1IyEKqFoEz4ZVmciE7i2aWw4i75Ml595MZ9AA4Z5N6X9ZCBP6lKlMK40ag8YWF6t6xGt8kPmt9CwPslzcYG2P9U6bXlBQICFUqNYirtH8VaOojmHUNLKYDg"));
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
