using server.Domain.Entities;

namespace server.Infrastructure.Interfaces;

public interface ITransactionItemRepository
{
    Task AddAsync(IList<TransactionItem> transactionItem);
}
