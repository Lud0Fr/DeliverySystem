using MediatR;

namespace DeliverySystem.Domain.Deliveries
{
    public class DeliveryCreatedEvent : INotification
    {
        public Delivery Delivery { get; private set; }

        private DeliveryCreatedEvent()
        { }

        public DeliveryCreatedEvent(Delivery delivery)
        {
            Delivery = delivery;
        }
    }
}
