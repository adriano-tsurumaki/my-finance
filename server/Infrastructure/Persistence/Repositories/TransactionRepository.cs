using Microsoft.EntityFrameworkCore;
using server.Domain.Entities;
using server.Infrastructure.Persistence.Context;
using server.Repositories.Interfaces;

namespace server.Infrastructure.Persistence.Repositories;

public class TransactionRepository(FinanceDbContext context) : ITransactionRepository
{
    private readonly FinanceDbContext _context = context;

    public async Task<ICollection<Transaction>> GetAllAsync()
    {
        return await _context.Transactions.ToListAsync();
    }

    public async Task<Transaction?> GetByIdAsync(long id)
    {
        return await _context.Transactions
            .FirstOrDefaultAsync(t => t.Id == id);
    }
}