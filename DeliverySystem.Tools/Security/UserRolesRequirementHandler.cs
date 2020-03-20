using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverySystem.Tools.Security
{
    public class UserRolesRequirementHandler : AuthorizationHandler<UserRolesRequirement>
    {
        private readonly IUserContext _userContext;

        public UserRolesRequirementHandler(IUserContext userContext)
        {
            _userContext = userContext;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            UserRolesRequirement requirement)
        {
            try
            {
                var userDetails = JsonConvert.DeserializeObject<UserDetails>(
                    context.User.Claims.Single(c => c.Type == IdentityClaims.UserDetailsKey).Value);

                _userContext.SetDetails(userDetails);

                context.Succeed(requirement);

                return Task.CompletedTask;
            }
            catch (Exception)
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
