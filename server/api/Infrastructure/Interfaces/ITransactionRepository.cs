using api.Domain.Entities;

namespace api.Repositories.Interfaces;

public interface ITransactionRepository
{
    Task<Transaction?> GetByIdAsync(Guid id);
    Task<ICollection<Transaction>> GetAllAsync();
    Task AddAsync(Transaction transaction);
}
