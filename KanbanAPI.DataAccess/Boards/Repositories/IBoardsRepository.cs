using KanbanAPI.DataAccess.Boards.Entities;
using KanbanAPI.DataAccess.Shared.DTOs;

namespace KanbanAPI.DataAccess.Boards.Repositories;

public interface IBoardsRepository
{
    Task<Board?> GetByIdAsync(Guid boardId, ContextGetParameters<Board> parameters);
}