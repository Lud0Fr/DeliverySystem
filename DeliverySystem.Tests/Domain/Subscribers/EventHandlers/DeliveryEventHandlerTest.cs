using DeliverySystem.Domain.Deliveries;
using DeliverySystem.Domain.Subscribers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using DeliveriesAccessWindow = DeliverySystem.Domain.Deliveries.AccessWindow;
using DeliveriesDelivery = DeliverySystem.Domain.Deliveries.Delivery;
using DeliveriesOrder = DeliverySystem.Domain.Deliveries.Order;
using DeliveriesRecipient = DeliverySystem.Domain.Deliveries.Recipient;
using SubscribersDelivery = DeliverySystem.Domain.Subscribers.Delivery;

namespace DeliverySystem.Tests.Domain.Subscribers.EventHandlers
{
    public class DeliveryEventHandlerTest
    {
        private readonly Mock<ISubscriberRepository> _subscriberRepository;
        private readonly Mock<ISubscriptionService> _subscriptionService;

        public DeliveryEventHandlerTest()
        {
            _subscriberRepository = new Mock<ISubscriberRepository>();
            _subscriptionService = new Mock<ISubscriptionService>();
        }

        [Fact]
        public async Task Handle_DeliveryCreatedEvent_Uses_GetAllByAsync_From_ISubscriberRepository()
        {
            // Arrange
            var sut = new DeliveryEventHandler(_subscriberRepository.Object, _subscriptionService.Object);
            // Act
            await sut.Handle(
                new DeliveryCreatedEvent(NewDelivery()),
                new CancellationToken());
            // Assert
            _subscriberRepository.Verify(r => r.GetAllByAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Handle_DeliveryCreatedEvent_Uses_SendDeliveryCreatedAsync_From_ISubscriptionService_For_Each_Subscriber()
        {
            // Arrange
            _subscriberRepository.Setup(r => r.GetAllByAsync(It.IsAny<int>())).ReturnsAsync(NewSubscribers());
            var sut = new DeliveryEventHandler(_subscriberRepository.Object, _subscriptionService.Object);
            // Act
            await sut.Handle(
                new DeliveryCreatedEvent(NewDelivery()),
                new CancellationToken());
            // Assert
            _subscriptionService.Verify(s => s.SendDeliveryCreatedAsync(
                It.IsAny<Subscriber>(),
                It.IsAny<SubscribersDelivery>()), Times.Exactly(2));
        }

        [Fact]
        public async Task Handle_DeliveryStateChangedEvent_Uses_GetAllByAsync_From_ISubscriberRepository()
        {
            // Arrange
            var sut = new DeliveryEventHandler(_subscriberRepository.Object, _subscriptionService.Object);
            // Act
            await sut.Handle(
                new DeliveryStateChangedEvent(It.IsAny<int>(), It.IsAny<int>(), DeliverySystem.Domain.Deliveries.DeliveryState.Approved, It.IsAny<DateTime>()),
                new CancellationToken());
            // Assert
            _subscriberRepository.Verify(r => r.GetAllByAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Handle_DeliveryStateChangedEvent_Uses_SendDeliveryCreatedAsync_From_ISubscriptionService_For_Each_Subscriber()
        {
            // Arrange
            _subscriberRepository.Setup(r => r.GetAllByAsync(It.IsAny<int>())).ReturnsAsync(NewSubscribers());
            var sut = new DeliveryEventHandler(_subscriberRepository.Object, _subscriptionService.Object);
            // Act
            await sut.Handle(
                new DeliveryStateChangedEvent(It.IsAny<int>(), It.IsAny<int>(), DeliverySystem.Domain.Deliveries.DeliveryState.Approved, It.IsAny<DateTime>()),
                new CancellationToken());
            // Assert
            _subscriptionService.Verify(s => s.SendDeliveryStateChangedAsync(
                It.IsAny<Subscriber>(),
                It.IsAny<int>(),
                It.IsAny<DeliverySystem.Domain.Subscribers.DeliveryState>(),
                It.IsAny<DateTime>()), Times.Exactly(2));
        }

        private DeliveriesDelivery NewDelivery()
        {
            return DeliveriesDelivery.New(
                new DeliveriesAccessWindow(DateTime.UtcNow, DateTime.UtcNow.AddHours(5)),
                new DeliveriesRecipient("name", "address", "email@domain.com", "+447000000000"),
                new DeliveriesOrder("123456", "Ikea"),
                userId: 333,
                partnerId: 222,
                createdBy: 1);
        }

        private IEnumerable<Subscriber> NewSubscribers()
        {
            var subscriber1 = Subscriber.New(222, "https://partner222.com/webhooks");
            subscriber1.AddSubscription(new Subscription(222, EventType.DeliveryCreated), 2);
            subscriber1.AddSubscription(new Subscription(222, EventType.DeliveryStateChanged), 2);

            var subscriber2 = Subscriber.New(444, "https://partner444.com/webhooks");
            subscriber2.AddSubscription(new Subscription(444, EventType.DeliveryCreated), 4);
            subscriber2.AddSubscription(new Subscription(444, EventType.DeliveryStateChanged), 4);

            yield return subscriber1;
            yield return subscriber2;
        }
    }
}
