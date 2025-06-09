using server.Domain.Entities;
using server.Infrastructure.Interfaces;
using server.Infrastructure.Persistence.Context;

namespace server.Infrastructure.Persistence.Repositories;

public class TransactionItemRepository(FinanceDbContext financeDbContext) : ITransactionItemRepository
{
    private readonly FinanceDbContext _financeDbContext = financeDbContext;

    public async Task AddAsync(IList<TransactionItem> transactionItems)
    {
        foreach (var transactionItem in transactionItems)
        {
            await _financeDbContext.TransactionItems.AddAsync(transactionItem);
        }

        await _financeDbContext.SaveChangesAsync();
    }
}
