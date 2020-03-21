using MediatR;
using System;

namespace DeliverySystem.Domain.Deliveries
{
    public class DeliveryStateChangedEvent : INotification
    {
        public int DeliveryId { get; private set; }
        public int PartnerId { get; private set; }
        public DeliveryState State { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private DeliveryStateChangedEvent()
        { }

        public DeliveryStateChangedEvent(
            int deliveryId,
            int partnerId,
            DeliveryState state,
            DateTime updatedAt)
        {
            DeliveryId = deliveryId;
            PartnerId = partnerId;
            State = state;
            UpdatedAt = updatedAt;
        }
    }
}
