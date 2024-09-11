using AGSR.Domain.Entities;
using AGSR.Domain.Repositories.Base;
using AGSR.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace AGSR.Infrastructure.Repositories.Abstract;

internal class CrudRepository<TEntity> : ICrudRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly AppDbContext Context;

    protected CrudRepository(AppDbContext context)
    {
        Context = context;
    }

    public Task<TEntity?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return Context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id, token);
    }

    public Task<List<TEntity>> GetAllAsync(CancellationToken token = default)
    {
        return Context.Set<TEntity>().AsNoTracking().ToListAsync(token);
    }

    public void Create(TEntity entity)
    {
        Context.Set<TEntity>().Add(entity);
    }

    public void Update(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);
    }

    public void Delete(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
    }
}