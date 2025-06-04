namespace server.Domain.Entities;

public class Category : AuditableEntity
{
    public long Id { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<Transaction> Transactions { get; set; } = [];
}
