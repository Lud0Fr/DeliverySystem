using MediatR;

namespace DeliverySystem.Api.Commands
{
    public class DeleteDeliveryCommand : IRequest
    {
        public int DeliveryId { get; private set; }

        public DeleteDeliveryCommand(int deliveryId)
        {
            DeliveryId = deliveryId;
        }
    }
}
