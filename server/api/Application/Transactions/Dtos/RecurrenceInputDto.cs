using server.Domain.Enums;

namespace server.Application.Transactions.Dtos;

public class RecurrenceInputDto
{
    public bool IsActive { get; set; }
    public RecurrenceFrequency Frequency { get; set; } = RecurrenceFrequency.Monthly;
    public int Interval { get; set; } = 1;
    public DateTime NextDueDate { get; set; }
    public DateTime? EndDate { get; set; }
}
