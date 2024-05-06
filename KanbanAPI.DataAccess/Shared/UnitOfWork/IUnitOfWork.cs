using KanbanAPI.DataAccess.Boards.Repositories;

namespace KanbanAPI.DataAccess.Shared.UnitOfWork;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
    IBoardsRepository BoardsRepository { get; }
}