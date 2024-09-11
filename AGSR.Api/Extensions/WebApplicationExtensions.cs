using AGSR.Infrastructure.Database.Migrator;

namespace AGSR.Api.Extensions;

public static class WebApplicationExtensions
{
    public static void ApplyDatabaseMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var identityMigrator = scope.ServiceProvider.GetRequiredService<IDatabaseMigrator>();
        identityMigrator.Migrate();
    }
}