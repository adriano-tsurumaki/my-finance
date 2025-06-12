using System.ComponentModel;

namespace api.Domain.Enums;

public enum UserRole
{
    [Description("Admin")]
    Admin = 1,
    [Description("User")]
    User = 2
}
