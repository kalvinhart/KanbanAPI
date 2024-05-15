using KanbanAPI.DataAccess.Subtasks.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KanbanAPI.DataAccess.Subtasks.Config;

public class SubtaskConfiguration : IEntityTypeConfiguration<Subtask>
{
    public void Configure(EntityTypeBuilder<Subtask> builder)
    {
        builder.HasKey(e => e.SubtaskId);
        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(100);
        builder.HasOne(e => e.Card)
            .WithMany(e => e.Subtasks)
            .HasForeignKey(e => e.CardId)
            .IsRequired();
    }
}