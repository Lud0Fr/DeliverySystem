namespace DeliverySystem.Domain.Deliveries
{
    public class Delivery : AggregateRoot
    {
        public DeliveryState State { get; private set; }
        public AccessWindow AccessWindow { get; private set; }
        public Recipient Recipient { get; private set; }
        public Order Order { get; private set; }

        private Delivery() { }

        public Delivery New(
            AccessWindow accessWindow,
            Recipient recipient,
            Order order,
            int createdBy)
        {
            var delivery = new Delivery
            {
                AccessWindow = accessWindow
            };

            New(createdBy);

            return delivery;
        }
    }
}
