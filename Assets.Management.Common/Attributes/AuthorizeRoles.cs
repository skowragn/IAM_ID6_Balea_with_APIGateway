using Microsoft.AspNetCore.Authorization;

namespace Assets.Management.Common.Attributes;
public class AuthorizeRoles : AuthorizeAttribute
{
    public AuthorizeRoles(params string[] roles)
    {
        Roles = string.Join(",", roles);
    }
}