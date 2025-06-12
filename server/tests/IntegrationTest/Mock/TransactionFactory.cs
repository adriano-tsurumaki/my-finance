using api.Domain.Entities;
using api.Domain.Enums;

namespace IntegrationTest.Mock;

public class TransactionFactory : IEntityFactory<Transaction>
{
    public Transaction Create()
    {
        return new Transaction
        {
            TransactionId = Guid.NewGuid(),
            Amount = 100.00m,
            PaymentDate = DateTime.UtcNow,
            TransactionType = TransactionType.Expense,
            Status = TransactionStatus.Pending,
        };
    }
}
