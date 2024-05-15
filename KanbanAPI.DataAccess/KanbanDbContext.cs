using KanbanAPI.DataAccess.Boards.Config;
using KanbanAPI.DataAccess.Boards.Entities;
using KanbanAPI.DataAccess.Cards.Config;
using KanbanAPI.DataAccess.Cards.Entities;
using KanbanAPI.DataAccess.Columns.Config;
using KanbanAPI.DataAccess.Columns.Entities;
using KanbanAPI.DataAccess.Subtasks.Config;
using KanbanAPI.DataAccess.Subtasks.Entities;
using Microsoft.EntityFrameworkCore;

namespace KanbanAPI.DataAccess;

public class KanbanDbContext : DbContext
{
    public KanbanDbContext(DbContextOptions<KanbanDbContext> options) : base(options)
    {
    }

    public DbSet<Board> Boards { get; set; }
    public DbSet<Column> Columns { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Subtask> Subtasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BoardConfiguration());
        modelBuilder.ApplyConfiguration(new ColumnConfiguration());
        modelBuilder.ApplyConfiguration(new CardConfiguration());
        modelBuilder.ApplyConfiguration(new SubtaskConfiguration());
    }
}