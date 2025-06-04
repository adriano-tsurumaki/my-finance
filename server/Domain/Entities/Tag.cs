namespace server.Domain.Entities;

public class Tag : AuditableEntity
{
    public long Id { get; set; }
    public Guid TagId { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<TransactionTag>? TransactionTags { get; set; } = [];
}
