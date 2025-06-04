using server.Domain.Entities;

namespace server.Repositories.Interfaces;

public interface ITransactionRepository
{
    Task<Transaction?> GetByIdAsync(long id);
    Task<ICollection<Transaction>> GetAllAsync();
}
