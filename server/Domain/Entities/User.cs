using server.Domain.Enums;

namespace server.Domain.Entities;

public class User : AuditableEntity
{
    public long Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.User;
    public DateTime? LastLogin { get; set; } = null;
    public bool IsActive { get; set; } = true;
    public string Locale { get; set; } = "en-US";
    public string TimeZone { get; set; } = "UTC";

    public ICollection<Category>? Categories { get; set; }
    public ICollection<Transaction> Transactions { get; set; } = [];
}
