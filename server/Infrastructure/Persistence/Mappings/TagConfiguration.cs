using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using server.Domain.Entities;

namespace server.Infrastructure.Persistence.Mappings;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.TagId).IsUnique();

        builder.Property(e => e.Name).IsRequired().HasMaxLength(50);

        builder.HasMany(e => e.TransactionTags)
               .WithOne(tt => tt.Tag)
               .HasForeignKey(tt => tt.TagId);
    }
}
