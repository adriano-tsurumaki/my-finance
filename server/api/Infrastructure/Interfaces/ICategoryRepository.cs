using server.Domain.Entities;

namespace server.Infrastructure.Interfaces;

public interface ICategoryRepository
{
    Task<Category?> GetByNameAndUserIdAsync(string name, long userId);
    Task AddAsync(Category category);
}
