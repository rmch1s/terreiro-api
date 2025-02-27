using Terreiro.Application.Services.SetPin;

namespace Terreiro.Presentation.Configuration;

public static class ApplicationConfiguration
{
    public static void AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddScoped<ISetPinService, SetPinService>();
    }
}
