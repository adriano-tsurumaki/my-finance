using server.Domain.Entities;

namespace server.Infrastructure.Interfaces;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User?> GetByEmailAsync(string email);
}
