using server.Domain.Entities;

namespace server.Infrastructure.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
