namespace DeliverySystem.Tools.Security
{
    public class UserDetails
    {
        public int IdentityId { get; set; }
        public Role Role { get; set; }

        public static UserDetails New(
            int identityId,
            Role role)
        {
            return new UserDetails
            {
                IdentityId = identityId,
                Role = role,
            };
        }
    }
}
