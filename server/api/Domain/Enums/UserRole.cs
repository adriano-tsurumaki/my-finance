using System.ComponentModel;

namespace server.Domain.Enums;

public enum UserRole
{
    [Description("Admin")]
    Admin = 1,
    [Description("User")]
    User = 2
}
