using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using server.Domain.Entities;

namespace server.Infrastructure.Persistence.Mappings;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.CategoryId).IsUnique();

        builder.Property(c => c.Name).IsRequired().HasMaxLength(50);

        builder.HasOne(c => c.User)
               .WithMany(u => u.Categories)
               .HasForeignKey(c => c.UserId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(c => c.Transactions)
               .WithOne(t => t.Category)
               .HasForeignKey(t => t.CategoryId);
    }
}
