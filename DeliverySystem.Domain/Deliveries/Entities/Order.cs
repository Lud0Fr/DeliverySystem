using Microsoft.EntityFrameworkCore;

namespace DeliverySystem.Domain.Deliveries
{
    [Owned]
    public class Order
    {
        public string OrderNumber { get; private set; }
        public string Sender { get; private set; }

        private Order() { }

        public Order(string orderNumber, string sender)
        {
            OrderNumber = orderNumber;
            Sender = sender;
        }
    }
}
