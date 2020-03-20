using Microsoft.AspNetCore.Authorization;

namespace DeliverySystem.Tools.Security
{
    public class UserRolesRequirement : IAuthorizationRequirement
    {
        public const string PolicyKey = "User_Roles_RequirementKey";
    }
}
