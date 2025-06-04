using server.Domain.Enums;

namespace server.Domain.Entities;

public class Transaction : AuditableEntity
{
    public long Id { get; set; }
    public Guid TransactionId { get; set; }
    
    public string PaymentMethod { get; set; } = string.Empty; // e.g., "Credit Card", "PayPal"
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }

    public TransactionType TransactionType { get; set; }
    public TransactionStatus Status { get; set; }

    public long UserId { get; set; }
    public User User { get; set; } = null!;

    public long? RecurrenceId { get; set; }
    public Recurrence? Recurrence { get; set; }

    public long? CategoryId { get; set; }
    public Category? Category { get; set; }

    public long? InstallmentPlanId { get; set; }
    public InstallmentPlan? InstallmentPlan { get; set; }

    public ICollection<TransactionItem>? TransactionItems { get; set; }
    public ICollection<TransactionTag>? TransactionTags { get; set; }
}
