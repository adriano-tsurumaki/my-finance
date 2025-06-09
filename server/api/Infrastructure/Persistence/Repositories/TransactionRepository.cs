using Microsoft.EntityFrameworkCore;
using api.Domain.Entities;
using api.Infrastructure.Persistence.Context;
using api.Repositories.Interfaces;

namespace api.Infrastructure.Persistence.Repositories;

public class TransactionRepository(FinanceDbContext context) : ITransactionRepository
{
    private readonly FinanceDbContext _context = context;

    public async Task<ICollection<Transaction>> GetAllAsync()
    {
        return await _context.Transactions.ToListAsync();
    }

    public async Task<Transaction?> GetByIdAsync(Guid id)
    {
        return await _context.Transactions
            .FirstOrDefaultAsync(t => t.TransactionId == id);
    }

    public async Task AddAsync(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
    }
}