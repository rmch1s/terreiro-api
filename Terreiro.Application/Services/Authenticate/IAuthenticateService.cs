using Terreiro.Domain.Entities;

namespace Terreiro.Application.Services.Authenticate;

public interface IAuthenticateService
{
    string GenerateToken(User user);
}
