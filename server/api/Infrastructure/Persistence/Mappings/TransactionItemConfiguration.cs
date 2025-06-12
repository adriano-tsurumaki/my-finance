using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using api.Domain.Entities;

namespace api.Infrastructure.Persistence.Mappings;

public class TransactionItemConfiguration : IEntityTypeConfiguration<TransactionItem>
{
    public void Configure(EntityTypeBuilder<TransactionItem> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.TransactionItemId).IsUnique();

        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Quantity).HasColumnType("decimal(18,2)");
        builder.Property(e => e.UnitPrice).HasColumnType("decimal(18,2)");
        builder.Property(e => e.UnitOfMeasure).HasMaxLength(20);
        builder.Ignore(e => e.Total);

        builder.HasOne(e => e.Transaction)
               .WithMany(t => t.TransactionItems)
               .HasForeignKey(e => e.TransactionId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.InstallmentPlan)
               .WithMany(i => i.TransactionItems)
               .HasForeignKey(e => e.InstallmentPlanId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
