using DeliverySystem.Tools.Domain;

namespace DeliverySystem.Domain.Subscribers
{
    public class Subscription : Entity
    {
        public int SubscriberId { get; set; }
        public EventType EventType { get; private set; }

        private Subscription()
        { }

        public Subscription(
            int subscriberId,
            EventType eventType)
        {
            SubscriberId = subscriberId;
            EventType = eventType;
        }

        public Subscription WithId(int id)
        {
            Id = id;

            return this;
        }
    }
}
