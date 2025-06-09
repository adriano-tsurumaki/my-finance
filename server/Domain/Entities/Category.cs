namespace server.Domain.Entities;

public class Category : AuditableEntity
{
    public long Id { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;

    public long UserId { get; set; }
    public User User { get; set; } = null!;

    public ICollection<Transaction> Transactions { get; set; } = [];
}
