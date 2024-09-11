using System.Reflection;
using AGSR.Domain.Entities;
using AGSR.Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AGSR.Infrastructure.Database;

internal class AppDbContext : DbContext, IUnitOfWork
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Patient> Patients { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}