namespace Terreiro.Domain.Entities;

public class UserRole(int userId, int roleId) : Entity
{
    public int UserId { get; } = userId;
    public int RoleId { get; } = roleId;

    public virtual User User { get; } = default!;
    public virtual Role Role { get; } = default!;
}
