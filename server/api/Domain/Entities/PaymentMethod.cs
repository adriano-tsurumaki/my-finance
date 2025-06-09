using server.Domain.Enums;

namespace server.Domain.Entities;

public class PaymentMethod : AuditableEntity
{
    public long Id { get; set; }
    public Guid PaymentMethodId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Locale { get; set; } = string.Empty;
    public PaymentMethodType Identifier { get; set; }

    public ICollection<Transaction> Transactions { get; set; } = [];
}
