using Terreiro.Application.Services.SetPin;
using Terreiro.Application.Services.UpdateUserEvent;

namespace Terreiro.Presentation.Configuration;

public static class ApplicationConfiguration
{
    public static void AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddScoped<ISetPinService, SetPinService>();
        services.AddScoped<IUpdateUserEventService, UpdateUserEventService>();
    }
}
