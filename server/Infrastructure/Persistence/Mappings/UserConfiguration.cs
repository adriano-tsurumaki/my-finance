using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using server.Domain.Entities;

namespace server.Infrastructure.Persistence.Mappings;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasIndex(e => e.UserId).IsUnique();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Email).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Password).IsRequired();
        builder.Property(e => e.Role).HasDefaultValue("User");
        builder.Property(e => e.Locale).HasDefaultValue("en-US");
        builder.Property(e => e.TimeZone).HasDefaultValue("UTC");

        builder.HasMany(e => e.Transactions)
               .WithOne(t => t.User)
               .HasForeignKey(t => t.UserId);
    }
}
