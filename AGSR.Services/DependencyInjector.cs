using AGSR.Services.Contracts;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace AGSR.Services;

public static class DependencyInjector
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddMapster();

        services.AddScoped<IPatientsService, PatientsService>();
        
        return services;
    }
}