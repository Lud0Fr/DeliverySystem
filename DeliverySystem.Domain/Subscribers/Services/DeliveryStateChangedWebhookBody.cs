using System;

namespace DeliverySystem.Domain.Subscribers
{
    public class DeliveryStateChangedWebhookBody : WebhookBody
    {
        public int DeliveryId { get; set; }
        public DeliveryState State { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
