namespace Terreiro.Domain.Entities;

public class UserRole
{
    private UserRole() { }

    public UserRole(int userId, int roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }

    public int UserId { get; }
    public int RoleId { get; }

    public virtual User User { get; } = default!;
    public virtual Role Role { get; } = default!;
}
