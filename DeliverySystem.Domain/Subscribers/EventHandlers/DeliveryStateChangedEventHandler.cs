using System.Threading;
using System.Threading.Tasks;
using DeliverySystem.Domain.Deliveries;
using MediatR;

namespace DeliverySystem.Domain.Subscribers
{
    public class DeliveryStateChangedEventHandler :
        INotificationHandler<DeliveryCreatedEvent>,
        INotificationHandler<DeliveryStateChangedEvent>
    {
        public DeliveryStateChangedEventHandler()
        {

        }

        public Task Handle(DeliveryCreatedEvent notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task Handle(DeliveryStateChangedEvent notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
