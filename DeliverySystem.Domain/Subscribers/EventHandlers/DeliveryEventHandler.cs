using DeliverySystem.Domain.Deliveries;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DeliverySystem.Domain.Subscribers
{
    public class DeliveryEventHandler :
        INotificationHandler<DeliveryCreatedEvent>,
        INotificationHandler<DeliveryStateChangedEvent>
    {
        private readonly ISubscriberRepository _subscriberRepository;
        private readonly ISubscriptionService _subscriptionService;

        public DeliveryEventHandler(
            ISubscriberRepository subscriberRepository,
            ISubscriptionService subscriptionService)
        {
            _subscriberRepository = subscriberRepository;
            _subscriptionService = subscriptionService;
        }

        public async Task Handle(DeliveryCreatedEvent notification, CancellationToken cancellationToken)
        {
            var subscribers = await _subscriberRepository.GetAllByAsync(notification.Delivery.PartnerId);

            foreach (var subscriber in subscribers)
            {
                if (SubscribeToEvent(subscriber, EventType.DeliveryCreated))
                {
                    await _subscriptionService.SendDeliveryCreatedAsync(
                        subscriber,
                        new Delivery(
                            new AccessWindow(
                                notification.Delivery.AccessWindow.StartTime,
                                notification.Delivery.AccessWindow.EndTime),
                            new Recipient(
                                notification.Delivery.Recipient.Name,
                                notification.Delivery.Recipient.Address,
                                notification.Delivery.Recipient.Email,
                                notification.Delivery.Recipient.PhoneNumber),
                            new Order(
                                notification.Delivery.Order.OrderNumber,
                                notification.Delivery.Order.Sender),
                            notification.Delivery.CreatedAt));
                }
            }
        }

        public async Task Handle(DeliveryStateChangedEvent notification, CancellationToken cancellationToken)
        {
            var subscribers = await _subscriberRepository.GetAllByAsync(notification.PartnerId);

            foreach (var subscriber in subscribers)
            {
                if (SubscribeToEvent(subscriber, EventType.DeliveryStateChanged))
                {
                    await _subscriptionService.SendDeliveryStateChangedAsync(
                        subscriber,
                        notification.DeliveryId,
                        (DeliveryState)notification.State,
                        notification.UpdatedAt);
                }
            }
        }

        private static bool SubscribeToEvent(Subscriber subscriber, EventType eventType)
        {
            return subscriber.Subscriptions.Where(s => s.EventType == eventType).Any();
        }
    }
}
