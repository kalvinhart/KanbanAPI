using KanbanAPI.DataAccess.Shared.DTOs;

namespace KanbanAPI.DataAccess.Shared.Repositories.GenericRepository;

public interface IGenericRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<List<T>> GetAllAsync(ContextGetParameters<T> parameters);
    Task<T?> GetByIdAsync(Guid id);
    Task AddAsync(T entity);
    void Remove(T entity);
}