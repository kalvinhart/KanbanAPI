using KanbanAPI.DataAccess.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace KanbanAPI.DataAccess.Shared.Repositories.GenericRepository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(DbContext dbContext)
    {
        _dbSet = dbContext.Set<T>();
    }

    public virtual async Task<List<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<List<T>> GetAllAsync(ContextGetParameters<T> parameters)
    {
        IQueryable<T> query = _dbSet;
        query = Filter(query, parameters);

        return await query.ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public virtual void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    protected IQueryable<T> Filter(IQueryable<T> query, ContextGetParameters<T> parameters)
    {
        if (parameters.Includes is not null) query = parameters.Includes(query);
        if (parameters.DisableTracking) query = query.AsNoTracking();

        return query;
    }
}