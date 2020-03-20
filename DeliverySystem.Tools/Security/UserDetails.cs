namespace DeliverySystem.Tools.Security
{
    public class UserDetails
    {
        public int Id { get; private set; }
        public Role Role { get; private set; }

        private UserDetails()
        { }

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
