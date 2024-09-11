using AGSR.Domain.Repositories;
using AGSR.Domain.Repositories.Base;
using AGSR.Infrastructure.Database;
using AGSR.Infrastructure.Database.Migrator;
using AGSR.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AGSR.Infrastructure;

public static class DependencyInjector
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<IUnitOfWork, AppDbContext>(opt =>
        {
            opt.UseNpgsql(config["ConnectionString"]);
            opt.EnableDetailedErrors();
        });

        services.AddScoped<IDatabaseMigrator, DatabaseMigrator>();

        services.AddRepositories();

        return services;
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPatientsRepository, PatientsRepository>();
    }
}