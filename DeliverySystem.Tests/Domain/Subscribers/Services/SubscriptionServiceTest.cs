using DeliverySystem.Domain.Subscribers;
using Moq;
using System;
using Xunit;

namespace DeliverySystem.Tests.Domain.Subscribers.Services
{
    public class SubscriptionServiceTest
    {
        [Fact]
        public void SendDeliveryCreatedAsync_Throws_An_Exception_When_HttpClient_Post_Fails()
        {
            // Arrange
            var sut = new SubscriptionService();
            // Assert
            Assert.ThrowsAsync<Exception>(() => sut.SendDeliveryCreatedAsync(It.IsAny<Subscriber>(), It.IsAny<Delivery>()));
        }

        [Fact]
        public void SendDeliveryStateChangedAsync_Throws_An_Exception_When_HttpClient_Post_Fails()
        {
            // Arrange
            var sut = new SubscriptionService();
            // Assert
            Assert.ThrowsAsync<Exception>(() => sut.SendDeliveryStateChangedAsync(
                It.IsAny<Subscriber>(),
                It.IsAny<int>(),
                DeliveryState.Approved,
                It.IsAny<DateTime>()));
        }
    }
}
