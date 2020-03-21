using System;

namespace DeliverySystem.Domain.Subscribers
{
    public abstract class WebhookBody
    {
        public Guid EventId { get; private set; }
        public DateTime SentAt { get; private set; }

        public WebhookBody()
        {
            EventId = Guid.NewGuid();
            SentAt = DateTime.UtcNow;
        }
    }
}
