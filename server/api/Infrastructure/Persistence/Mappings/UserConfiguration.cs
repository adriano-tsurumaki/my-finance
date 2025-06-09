using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using api.Domain.Entities;
using api.Domain.Enums;

namespace api.Infrastructure.Persistence.Mappings;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasIndex(e => e.UserId).IsUnique();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        
        builder.Property(e => e.Email).IsRequired().HasMaxLength(100);
        builder.HasIndex(e => e.Email).IsUnique();

        builder.Property(e => e.Password).IsRequired();
        builder.Property(e => e.Role);
        builder.Property(e => e.Locale).HasDefaultValue("en-US");
        builder.Property(e => e.TimeZone).HasDefaultValue("UTC");

        builder.HasMany(e => e.Categories)
               .WithOne(c => c.User)
               .HasForeignKey(c => c.UserId);

        builder.HasMany(e => e.Transactions)
               .WithOne(t => t.User)
               .HasForeignKey(t => t.UserId);
    }
}
