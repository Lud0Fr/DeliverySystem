using MediatR;

namespace DeliverySystem.Api.Commands
{
    public class CompleteDeliveryCommand : IRequest
    {
        public int DeliveryId { get; private set; }

        public CompleteDeliveryCommand(int deliveryId)
        {
            DeliveryId = deliveryId;
        }
    }
}
