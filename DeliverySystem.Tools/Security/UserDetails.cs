namespace DeliverySystem.Tools.Security
{
    public class UserDetails
    {
        public int Id { get; set; }
        public Role Role { get; set; }
        public int? UserConsumerMarketId { get; set; }
        public int? PartnerId { get; set; }

        public static UserDetails New(
            int id,
            Role role,
            int? userConsumerMarketId = null,
            int? partnerId = null)
        {
            return new UserDetails
            {
                Id = id,
                Role = role,
                UserConsumerMarketId = userConsumerMarketId,
                PartnerId = partnerId
            };
        }
    }
}
