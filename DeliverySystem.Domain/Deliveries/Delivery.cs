using DeliverySystem.Tools.Domain;

namespace DeliverySystem.Domain.Deliveries
{
    public class Delivery : AggregateRoot
    {
        public DeliveryState State { get; private set; }
        public AccessWindow AccessWindow { get; private set; }
        public Recipient Recipient { get; private set; }
        public Order Order { get; private set; }
        public int UserId { get; private set; }
        public int PartnerId { get; private set; }

        private Delivery() { }

        public static Delivery New(
            AccessWindow accessWindow,
            Recipient recipient,
            Order order,
            int userId,
            int partnerId,
            int createdBy)
        {
            var delivery = new Delivery
            {
                AccessWindow = accessWindow,
                Order = order,
                Recipient = recipient,
                UserId = userId,
                PartnerId = partnerId
            };

            delivery.New(createdBy);

            return delivery;
        }
    }
}
