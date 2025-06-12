using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using api.Domain.Entities;

namespace api.Infrastructure.Persistence.Mappings;

public class RecurrenceConfiguration : IEntityTypeConfiguration<Recurrence>
{
    public void Configure(EntityTypeBuilder<Recurrence> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.RecurrenceId).IsUnique();

        builder.Property(e => e.Frequency)
               .HasConversion<string>();

        builder.HasMany(e => e.Transactions)
               .WithOne(t => t.Recurrence)
               .HasForeignKey(t => t.RecurrenceId);
    }
}
