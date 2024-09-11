using AGSR.Domain.Entities;
using AGSR.Domain.Repositories;
using AGSR.Infrastructure.Database;
using AGSR.Infrastructure.Extensions;
using AGSR.Infrastructure.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace AGSR.Infrastructure.Repositories;

internal class PatientsRepository : CrudRepository<Patient>, IPatientsRepository
{
    public PatientsRepository(AppDbContext context) : base(context)
    {
    }

    public Task<List<Patient>> GetAllAsync(IEnumerable<string> dateFilters, CancellationToken token = default)
    {
        var query = Context.Patients.AsNoTracking();

        return dateFilters
            .Aggregate(query, (q, filter) => q.ApplyFhirDateFilter(x => x.BirthDate, filter))
            .OrderByDescending(x => x.BirthDate)
            .ToListAsync(token);
    }
}