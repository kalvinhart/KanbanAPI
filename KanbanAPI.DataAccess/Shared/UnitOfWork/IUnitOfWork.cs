namespace KanbanAPI.DataAccess.Shared.UnitOfWork;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}