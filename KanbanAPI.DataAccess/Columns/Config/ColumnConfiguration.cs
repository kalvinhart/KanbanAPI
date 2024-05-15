using KanbanAPI.DataAccess.Columns.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KanbanAPI.DataAccess.Columns.Config;

public class ColumnConfiguration : IEntityTypeConfiguration<Column>
{
    public void Configure(EntityTypeBuilder<Column> builder)
    {
        builder.HasKey(e => e.ColumnId);
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);
        builder.HasOne(e => e.Board)
            .WithMany(e => e.Columns)
            .HasForeignKey(e => e.BoardId)
            .IsRequired();
        builder.HasMany(e => e.Cards)
            .WithOne(e => e.Column);
    }
}