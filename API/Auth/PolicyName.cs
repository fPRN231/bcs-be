using Domain.Constants;

namespace API.Auth;

public static class PolicyName 
{
    public const string ADMIN = nameof(Role.Admin);
    public const string CUSTOMER = nameof(Role.Customer);
    public const string DOCTOR = nameof(Role.Doctor);
    public const string GUEST = nameof(Role.Guest);
    public const string STAFF = nameof(Role.Staff);
}

