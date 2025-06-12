using api.Domain.Entities;

namespace IntegrationTest.Mock;

public class TransactionItemFactory : IEntityFactory<TransactionItem>
{
    public TransactionItem Create()
    {
        return new TransactionItem
        {
            TransactionItemId = Guid.NewGuid(),
            Name = "Sample Item",
            Quantity = 1,
            UnitPrice = 10.00m,
            UnitOfMeasure = "pcs",
        };
    }
}
