using Terreiro.Domain.Entities;

namespace Terreiro.Application.Services.UpdateUserRole;

public interface IUpdateUserRoleService
{
    Task<(int, Role?)> Update(User user, Role role);
}