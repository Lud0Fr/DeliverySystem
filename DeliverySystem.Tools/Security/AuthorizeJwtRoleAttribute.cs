using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace DeliverySystem.Tools.Security
{
    public class AuthorizeJwtRoleAttribute : AuthorizeAttribute
    {
        public AuthorizeJwtRoleAttribute(params string[] roles) : base()
        {
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
            Policy = UserRolesRequirement.PolicyKey;

            if (roles != null && roles.Any())
                Roles = string.Join(',', roles);
        }
    }
}
