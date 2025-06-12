using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using api.Domain.Entities;

namespace api.Infrastructure.Persistence.Mappings;

public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.HasIndex(e => e.PaymentMethodId).IsUnique();
        
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Locale)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(e => e.Identifier)
            .IsRequired()
            .HasConversion<int>();
    }
}
