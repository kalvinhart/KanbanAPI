using KanbanAPI.DataAccess.Boards.Entities;
using KanbanAPI.DataAccess.Shared.DTOs;
using KanbanAPI.DataAccess.Shared.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace KanbanAPI.DataAccess.Boards.Repositories;

public class BoardsRepository : GenericRepository<Board>, IBoardsRepository
{
    public BoardsRepository(DbContext dbContext) : base(dbContext)
    {

    }

    public Task<Board?> GetByIdAsync(Guid boardId, ContextGetParameters<Board> parameters)
    {
        IQueryable<Board> query = _dbSet.AsQueryable();
        query = Filter(query, parameters);

        return query.FirstOrDefaultAsync(s => s.BoardId == boardId);
    }
}