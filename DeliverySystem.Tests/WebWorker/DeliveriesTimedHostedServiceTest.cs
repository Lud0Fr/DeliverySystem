using DeliverySystem.Domain.Deliveries;
using DeliverySystem.Infrastructure;
using DeliverySystem.WebWorker.TimedHostedServices;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace DeliverySystem.Tests.WebWorker
{
    public class DeliveriesTimedHostedServiceTest : TestBase
    {
        private readonly Mock<IDeliveryRepository> _deliveryRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public DeliveriesTimedHostedServiceTest()
        {
            _deliveryRepository = new Mock<IDeliveryRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task ProcessAsync_Uses_GetAllAsync_From_IDeliveryRepository_To_Get_The_Ended_Deliveries()
        {
            // Arrange
            var sut = new DeliveriesTimedHostedService(Configuration, _deliveryRepository.Object, _unitOfWork.Object);
            // Act
            await sut.ProcessAsync(null);
            // Assert
            _deliveryRepository.Verify(r => r.GetAllAsync(It.IsAny<Expression<Func<Delivery, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task ProcessAsync_Uses_Update_From_IDeliveryRepository_As_Many_Times_As_Deliveries_Retrieved_To_Update_The_Expired_Deliveries()
        {
            // Arrange
            _deliveryRepository.Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Delivery, bool>>>())).ReturnsAsync(NewDeliveries());
            var sut = new DeliveriesTimedHostedService(Configuration, _deliveryRepository.Object, _unitOfWork.Object);
            // Act
            await sut.ProcessAsync(null);
            // Assert
            _deliveryRepository.Verify(r => r.Update(It.Is<Delivery>(d => d.State == DeliveryState.Expired)), Times.Exactly(2));
        }

        [Fact]
        public async Task ProcessAsync_Uses_SaveAllAsync_From_IUnitOfWork()
        {
            // Arrange
            var sut = new DeliveriesTimedHostedService(Configuration, _deliveryRepository.Object, _unitOfWork.Object);
            // Act
            await sut.ProcessAsync(null);
            // Assert
            _unitOfWork.Verify(u => u.SaveAllAsync(), Times.Once);
        }

        private IEnumerable<Delivery> NewDeliveries()
        {
            yield return Delivery.New(
                   new AccessWindow(DateTime.UtcNow.AddHours(-10), DateTime.UtcNow.AddHours(-5)),
                   new Recipient("name", "address", "email", "+447000000000"),
                   new Order("123456", "Ikea"),
                   userId: 3,
                   partnerId: 2,
                   createdBy: 1);

            yield return Delivery.New(
                   new AccessWindow(DateTime.UtcNow.AddHours(-20), DateTime.UtcNow.AddHours(-15)),
                   new Recipient("name", "address", "email", "+447000000000"),
                   new Order("654321", "Ikea"),
                   userId: 3,
                   partnerId: 2,
                   createdBy: 1);
        }
    }
}
