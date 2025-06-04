namespace server.Domain.Entities;

public class TransactionItem : AuditableEntity
{
    public long Id { get; set; }
    public Guid TransactionItemId { get; set; }

    public string Name { get; set; } = string.Empty;
    public decimal Quantity { get; set; } = 1;
    public string UnitOfMeasure { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public decimal Total => Quantity * UnitPrice;

    public Guid TransactionId { get; set; }
    public Transaction Transaction { get; set; } = null!;
}
