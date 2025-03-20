using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Terreiro.Domain.Entities;

namespace Terreiro.Application.Services.Authenticate;

internal class AuthenticateService(IConfiguration configuration) : IAuthenticateService
{
    public string GenerateToken(User user)
    {
        var claims = new List<Claim>()
        {
            new ("id", user.Id.ToString()),
            new ("name", user.Name),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        List<Claim> claimRoles = [];
        foreach (var role in user.Roles)
            claimRoles.Add(new Claim(ClaimTypes.Role, role.Name));

        claims.AddRange(claimRoles);

        var privateKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!));

        var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

        var expiration = DateTime.UtcNow.AddHours(configuration.GetValue<int>("Jwt:ExpirationHours"));

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
