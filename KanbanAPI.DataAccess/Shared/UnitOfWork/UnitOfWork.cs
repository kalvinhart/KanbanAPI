using KanbanAPI.DataAccess.Boards.Repositories;

namespace KanbanAPI.DataAccess.Shared.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly KanbanDbContext _context;
    public IBoardsRepository BoardsRepository { get; }

    public UnitOfWork(KanbanDbContext context)
    {
        _context = context;
        BoardsRepository = new BoardsRepository(context);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _context.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}