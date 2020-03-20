namespace DeliverySystem.Tools.Security
{
    public class UserDetails
    {
        public int Id { get; set; }
        public Role Role { get; set; }

        public static UserDetails New(
            int id,
            Role role)
        {
            return new UserDetails
            {
                Id = id,
                Role = role,
            };
        }
    }
}
