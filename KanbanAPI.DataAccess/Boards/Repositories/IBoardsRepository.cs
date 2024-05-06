using KanbanAPI.DataAccess.Boards.Entities;
using KanbanAPI.DataAccess.Shared.DTOs;
using KanbanAPI.DataAccess.Shared.Repositories.GenericRepository;

namespace KanbanAPI.DataAccess.Boards.Repositories;

public interface IBoardsRepository : IGenericRepository<Board>
{
    Task<Board?> GetByIdAsync(Guid boardId, ContextGetParameters<Board> parameters);
    Task<bool> BoardExists(string name);
}