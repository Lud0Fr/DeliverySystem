using MediatR;

namespace DeliverySystem.Api.Commands
{
    public class CancelDeliveryCommand : IRequest
    {
        public int DeliveryId { get; private set; }

        public CancelDeliveryCommand(int deliveryId)
        {
            DeliveryId = deliveryId;
        }
    }
}
