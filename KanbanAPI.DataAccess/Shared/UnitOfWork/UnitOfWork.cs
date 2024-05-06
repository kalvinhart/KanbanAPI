namespace KanbanAPI.DataAccess.Shared.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly KanbanDbContext _context;

    public UnitOfWork(KanbanDbContext context)
    {
        _context = context;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}