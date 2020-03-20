using DeliverySystem.Tools;
using DeliverySystem.Tools.Domain;
using DeliverySystem.Tools.Security;

namespace DeliverySystem.Domain.Identities
{
    public class Identity : AggregateRoot
    {
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public Role Role { get; private set; }

        private Identity()
        { }

        public static Identity New(
            string email,
            string password,
            Role role)
        {
            return new Identity
            {
                Email = email,
                PasswordHash = password.ComputeSHA1(),
                Role = role,
            };
        }
        public Identity WithId(int id)
        {
            Id = id;

            return this;
        }
    }
}
