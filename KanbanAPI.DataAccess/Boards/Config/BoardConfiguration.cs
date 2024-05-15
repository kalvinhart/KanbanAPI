using KanbanAPI.DataAccess.Boards.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KanbanAPI.DataAccess.Boards.Config;

public class BoardConfiguration : IEntityTypeConfiguration<Board>
{
    public void Configure(EntityTypeBuilder<Board> builder)
    {
        builder.HasKey(e => e.BoardId);
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);
        builder.HasMany(e => e.Columns)
            .WithOne(e => e.Board);
    }
}