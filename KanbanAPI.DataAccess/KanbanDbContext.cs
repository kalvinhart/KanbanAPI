using KanbanAPI.DataAccess.Boards.Entities;
using KanbanAPI.DataAccess.Cards.Entities;
using KanbanAPI.DataAccess.Columns.Entities;
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
        modelBuilder.Entity<Board>(entity =>
        {
            entity.HasKey(e => e.BoardId);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            entity.HasMany(e => e.Columns)
                .WithOne(e => e.Board);
        });

        modelBuilder.Entity<Column>(entity =>
        {
            entity.HasKey(e => e.ColumnId);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            entity.HasOne(e => e.Board)
                .WithMany(e => e.Columns)
                .HasForeignKey(e => e.BoardId)
                .IsRequired();
            entity.HasMany(e => e.Cards)
                .WithOne(e => e.Column);
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.CardId);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Description)
                .HasMaxLength(500);
            entity.HasOne(e => e.Column)
                .WithMany(e => e.Cards)
                .HasForeignKey(e => e.ColumnId)
                .IsRequired();
        });

        modelBuilder.Entity<Subtask>(entity =>
        {
            entity.HasKey(e => e.SubtaskId);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);
            entity.HasOne(e => e.Card)
                .WithMany(e => e.Subtasks)
                .HasForeignKey(e => e.CardId)
                .IsRequired();
        });
    }
}