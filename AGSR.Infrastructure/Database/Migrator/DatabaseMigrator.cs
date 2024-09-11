using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AGSR.Infrastructure.Database.Migrator;

internal class DatabaseMigrator : IDatabaseMigrator
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<DatabaseMigrator> _logger;

    public DatabaseMigrator(AppDbContext dbContext, ILogger<DatabaseMigrator> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public void Migrate()
    {
        try
        {
            _dbContext.Database.Migrate();
            _logger.LogInformation("Database has been successfully migrated...");
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Exception occured while database migration:\r\n{message}", e.Message);
            throw;
        }
    }
}