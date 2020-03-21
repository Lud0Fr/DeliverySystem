namespace DeliverySystem.Domain.Subscribers
{
    public class DeliveryCreatedWebhookBody : WebhookBody
    {
        public Delivery Delivery { get; set; }
    }
}
