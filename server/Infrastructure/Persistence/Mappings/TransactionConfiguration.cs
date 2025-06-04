using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using server.Domain.Entities;

namespace server.Infrastructure.Persistence.Mappings;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.TransactionId).IsUnique();

        builder.Property(e => e.PaymentMethod).HasMaxLength(50);
        builder.Property(e => e.Amount).HasColumnType("decimal(18,2)");

        builder.HasOne(e => e.User)
               .WithMany(u => u.Transactions)
               .HasForeignKey(e => e.UserId);

        builder.HasOne(e => e.Category)
               .WithMany(c => c.Transactions)
               .HasForeignKey(e => e.CategoryId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.Recurrence)
               .WithMany(r => r.Transactions)
               .HasForeignKey(e => e.RecurrenceId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.InstallmentPlan)
               .WithMany(i => i.Transactions)
               .HasForeignKey(e => e.InstallmentPlanId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(e => e.TransactionItems)
               .WithOne(i => i.Transaction)
               .HasForeignKey(i => i.Id);

        builder.HasMany(e => e.TransactionTags)
               .WithOne(tt => tt.Transaction)
               .HasForeignKey(tt => tt.TransactionId);
    }
}
