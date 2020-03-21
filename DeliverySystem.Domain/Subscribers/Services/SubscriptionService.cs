using Newtonsoft.Json;
using Serilog;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DeliverySystem.Domain.Subscribers
{
    #region Interface

    public interface ISubscriptionService
    {
        Task SendDeliveryCreatedAsync(Subscriber subscriber, Delivery delivery);
        Task SendDeliveryStateChangedAsync(Subscriber subscriber, int deliveryId, DeliveryState state, DateTime updatedAt);
    }

    #endregion

    public class SubscriptionService : ISubscriptionService
    {
        protected static readonly HttpClient client = new HttpClient();

        public async Task SendDeliveryCreatedAsync(
            Subscriber subscriber,
            Delivery delivery)
        {
            var body = new DeliveryCreatedWebhookBody
            {
                Delivery = delivery,
            };

            await PostAsync(subscriber.NotificationUrl, body);
        }

        public async Task SendDeliveryStateChangedAsync(
            Subscriber subscriber,
            int deliveryId,
            DeliveryState state,
            DateTime updatedAt)
        {
            var body = new DeliveryStateChangedWebhookBody
            {
                DeliveryId = deliveryId,
                State = state,
                UpdatedAt = updatedAt
            };

            await PostAsync(subscriber.NotificationUrl, body);
        }

        private async Task PostAsync(string url, object body)
        {
            try
            {
                var response = await client.PostAsJsonAsync(url, body);

                Log.Information($"Webhook sent - Url: {url} Body: {JsonConvert.SerializeObject(body)}");

                if (!response.IsSuccessStatusCode)
                {
                    WebhookFailed(url, body, response.StatusCode.ToString());
                }
            }
            catch (Exception e)
            {
                WebhookFailed(url, body, e.Message);
            }
        }

        private void WebhookFailed(string url, object body, string error)
        {
            Log.Error($"Webhook failed - Error: {error} - Url: {url} Body: {JsonConvert.SerializeObject(body)}");
            //retry
        }
    }
}
