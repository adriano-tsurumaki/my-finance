using Microsoft.EntityFrameworkCore;
using api.Domain.Entities;
using api.Infrastructure.Interfaces;
using api.Infrastructure.Persistence.Context;

namespace api.Infrastructure.Persistence.Repositories;

public class CategoryRepository(FinanceDbContext financeDbContext) : ICategoryRepository
{
    private readonly FinanceDbContext _financeDbContext = financeDbContext;

    public async Task<Category?> GetByNameAndUserIdAsync(string name, long userId)
    {
        return await _financeDbContext.Categories
            .Include(c => c.Transactions)
            .FirstOrDefaultAsync(c => c.Name == name && c.UserId == userId);
    }

    public async Task AddAsync(Category category)
    {
        await _financeDbContext.Categories.AddAsync(category);
        await _financeDbContext.SaveChangesAsync();
    }
}
