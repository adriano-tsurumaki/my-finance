using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using api.Domain.Entities;

namespace api.Infrastructure.Persistence.Mappings;

public class InstallmentPlanConfiguration : IEntityTypeConfiguration<InstallmentPlan>
{
    public void Configure(EntityTypeBuilder<InstallmentPlan> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.InstallmentPlanId).IsUnique();

        builder.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");

        builder.HasMany(e => e.Transactions)
               .WithOne(t => t.InstallmentPlan)
               .HasForeignKey(t => t.InstallmentPlanId);
    }
}
