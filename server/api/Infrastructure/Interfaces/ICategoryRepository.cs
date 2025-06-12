using api.Domain.Entities;

namespace api.Infrastructure.Interfaces;

public interface ICategoryRepository
{
    Task<Category?> GetByNameAndUserIdAsync(string name, long userId);
    Task AddAsync(Category category);
}
