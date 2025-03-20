using Terreiro.Domain.Entities;

namespace Terreiro.Application.Services.UpsertUserRole;

public interface IUpsertUserRoleService
{
    Task<(int, Role?)> Upsert(User user, Role role);
}