using api.Domain.Entities;

namespace api.Infrastructure.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
