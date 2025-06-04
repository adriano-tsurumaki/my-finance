using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using server.Domain.Entities;

namespace server.Infrastructure.Persistence.Mappings;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.CategoryId).IsUnique();

        builder.Property(e => e.Name).IsRequired().HasMaxLength(50);

        builder.HasMany(e => e.Transactions)
               .WithOne(t => t.Category)
               .HasForeignKey(t => t.CategoryId);
    }
}
