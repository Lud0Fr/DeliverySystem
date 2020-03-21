using DeliverySystem.Tools.Domain;
using System.Collections.Generic;

namespace DeliverySystem.Domain.Subscribers
{
    public class Subscriber : AggregateRoot
    {
        public int PartnerId { get; private set; }
        public string NotificationUrl { get; private set; }
        public ICollection<Subscription> Subscriptions { get; set; }

        private Subscriber()
        { }

        public static Subscriber New(
            int partnerId,
            string notificationUrl)
        {
            var subscriber = new Subscriber
            {
                PartnerId = partnerId,
                NotificationUrl = notificationUrl
            };

            subscriber.New();

            return subscriber;
        }

        public void AddSubscription(
            Subscription subscription,
            int addedBy)
        {
            if (Subscriptions == null)
            {
                Subscriptions = new List<Subscription>();
            }

            Subscriptions.Add(subscription);

            Update(addedBy);
        }

        public Subscriber WithId(int id)
        {
            Id = id;

            return this;
        }
    }
}
