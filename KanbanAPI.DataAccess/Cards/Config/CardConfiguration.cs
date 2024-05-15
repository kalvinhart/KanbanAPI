using KanbanAPI.DataAccess.Cards.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KanbanAPI.DataAccess.Cards.Config;

public class CardConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.HasKey(e => e.CardId);
        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(e => e.Description)
            .HasMaxLength(500);
        builder.HasOne(e => e.Column)
            .WithMany(e => e.Cards)
            .HasForeignKey(e => e.ColumnId)
            .IsRequired();
    }
}