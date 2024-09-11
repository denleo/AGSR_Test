using AGSR.Domain.Entities;

namespace AGSR.Domain.Repositories.Base;

public interface ICrudRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<List<TEntity>> GetAllAsync(CancellationToken token = default);
    void Create(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}