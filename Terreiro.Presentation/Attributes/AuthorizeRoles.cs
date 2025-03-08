using Microsoft.AspNetCore.Authorization;
using Terreiro.Application.Enums;
using Terreiro.Application.Helpers;

namespace Terreiro.Presentation.Attributes;

public class AuthorizeRoles : AuthorizeAttribute
{
    public AuthorizeRoles(params EUserRole[] allowedRoles)
    {
        var roles = allowedRoles.Select(s => s.GetDescription()).ToList();
        Roles = string.Join(",", roles);
    }
}
