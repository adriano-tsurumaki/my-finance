using Microsoft.EntityFrameworkCore;
using server.Domain.Entities;
using server.Infrastructure.Interfaces;
using server.Infrastructure.Persistence.Context;

namespace server.Infrastructure.Persistence.Repositories;

public class UserRepository(FinanceDbContext context) : IUserRepository
{
    private readonly FinanceDbContext _context = context;

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public Task<User?> GetByIdAsync(Guid id)
    {
        return _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        return _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}
