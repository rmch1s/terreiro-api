using Microsoft.Extensions.DependencyInjection;
using Terreiro.Application.Services.Authenticate;
using Terreiro.Application.Services.SetPin;
using Terreiro.Application.Services.UpsertUserEvent;
using Terreiro.Application.Services.UpsertUserEventItem;
using Terreiro.Application.Services.UpsertUserRole;

namespace Terreiro.Application.Configurations;

public static class DependecyInjection
{
    public static void AddApplicationDependecyInjection(this IServiceCollection services)
    {
        services.AddScoped<ISetPinService, SetPinService>();
        services.AddScoped<IUpsertUserEventService, UpsertUserEventService>();
        services.AddScoped<IUpsertUserEventItemService, UpsertUserEventItemService>();
        services.AddScoped<IUpsertUserRoleService, UpsertUserRoleService>();
        services.AddScoped<IAuthenticateService, AuthenticateService>();
    }
}
