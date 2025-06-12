namespace api.Domain.Entities;

public class TransactionTag
{
    public long TransactionId { get; set; }
    public Transaction Transaction { get; set; } = null!;

    public long TagId { get; set; }
    public Tag Tag { get; set; } = null!;
}
