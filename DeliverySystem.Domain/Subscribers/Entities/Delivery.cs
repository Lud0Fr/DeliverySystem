using System;

namespace DeliverySystem.Domain.Subscribers
{
    public class Delivery
    {
        public AccessWindow AccessWindow { get; private set; }
        public Recipient Recipient { get; private set; }
        public Order Order { get; private set; }
        public DateTime CreatedAt { get; set; }

        private Delivery() { }

        public Delivery(
            AccessWindow accessWindow,
            Recipient recipient,
            Order order,
            DateTime createdAt)
        {
            AccessWindow = accessWindow;
            Order = order;
            Recipient = recipient;
            CreatedAt = createdAt;
        }
    }
}
