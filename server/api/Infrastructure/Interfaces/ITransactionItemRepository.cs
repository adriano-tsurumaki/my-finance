using api.Domain.Entities;

namespace api.Infrastructure.Interfaces;

public interface ITransactionItemRepository
{
    Task AddAsync(IList<TransactionItem> transactionItem);
}
