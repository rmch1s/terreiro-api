using Microsoft.Extensions.DependencyInjection;
using Terreiro.Application.Services.Authenticate;
using Terreiro.Application.Services.SetPin;
using Terreiro.Application.Services.UpdateUserEvent;
using Terreiro.Application.Services.UpdateUserEventItem;
using Terreiro.Application.Services.UpdateUserRole;

namespace Terreiro.Application.Configurations;

public static class DependecyInjection
{
    public static void AddApplicationDependecyInjection(this IServiceCollection services)
    {
        services.AddScoped<ISetPinService, SetPinService>();
        services.AddScoped<IUpdateUserEventService, UpdateUserEventService>();
        services.AddScoped<IUpdateUserEventItemService, UpdateUserEventItemService>();
        services.AddScoped<IUpdateUserRoleService, UpdateUserRoleService>();
        services.AddScoped<IAuthenticateService, AuthenticateService>();
    }
}
