using MediatR;

namespace DeliverySystem.Api.Commands
{
    public class ApproveDeliveryCommand : IRequest
    {
        public int DeliveryId { get; private set; }

        public ApproveDeliveryCommand(int deliveryId)
        {
            DeliveryId = deliveryId;
        }
    }
}
