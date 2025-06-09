namespace server.Domain.Entities;

public class InstallmentPlan : AuditableEntity
{
    public long Id { get; set; }
    public Guid InstallmentPlanId { get; set; }
    public decimal TotalAmount { get; set; }
    public int TotalInstallments { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int IntervalInMonths { get; set; }

    public ICollection<Transaction> Transactions { get; set; } = [];
    public ICollection<TransactionItem> TransactionItems { get; set; } = [];
}
