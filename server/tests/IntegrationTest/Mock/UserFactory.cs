using api.Domain.Entities;

namespace IntegrationTest.Mock;

public class UserFactory : IEntityFactory<User>
{
    public User Create()
    {
        return new User
        {
            UserId = Guid.NewGuid(),
            Name = "User",
            Email = $"user{Guid.NewGuid()}@example.com",
            Password = "123",
            Role = api.Domain.Enums.UserRole.User,
            LastLogin = null,
            IsActive = true,
            Locale = "en-US",
            TimeZone = "UTC"
        };
    }
}
