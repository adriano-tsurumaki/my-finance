using server.Domain.Entities;

namespace server.Repositories.Interfaces;

public interface ITransactionRepository
{
    Task<Transaction?> GetByIdAsync(Guid id);
    Task<ICollection<Transaction>> GetAllAsync();
    Task AddAsync(Transaction transaction);
}
