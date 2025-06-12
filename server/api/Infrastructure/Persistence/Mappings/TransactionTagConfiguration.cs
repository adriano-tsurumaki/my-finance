using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using api.Domain.Entities;

namespace api.Infrastructure.Persistence.Mappings;

public class TransactionTagConfiguration : IEntityTypeConfiguration<TransactionTag>
{
    public void Configure(EntityTypeBuilder<TransactionTag> builder)
    {
        builder.HasKey(tt => new { tt.TransactionId, tt.TagId });

        builder.HasOne(tt => tt.Transaction)
               .WithMany(t => t.TransactionTags)
               .HasForeignKey(tt => tt.TransactionId);

        builder.HasOne(tt => tt.Tag)
               .WithMany(t => t.TransactionTags)
               .HasForeignKey(tt => tt.TagId);
    }
}
