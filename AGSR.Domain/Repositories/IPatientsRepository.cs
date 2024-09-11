using AGSR.Domain.Entities;
using AGSR.Domain.Repositories.Base;

namespace AGSR.Domain.Repositories;

public interface IPatientsRepository : ICrudRepository<Patient>
{
    Task<List<Patient>> GetAllAsync(IEnumerable<string> dateFilters, CancellationToken token = default);
}