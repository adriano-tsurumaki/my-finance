using api.Domain.Enums;

namespace api.Domain.Entities;

public class Recurrence : AuditableEntity
{
    public long Id { get; set; }
    public Guid RecurrenceId { get; set; }
    public bool IsActive { get; set; }
    public RecurrenceFrequency Frequency { get; set; }
    public int Interval { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime NextDueDate { get; set; }

    public ICollection<Transaction> Transactions { get; set; } = [];
}
