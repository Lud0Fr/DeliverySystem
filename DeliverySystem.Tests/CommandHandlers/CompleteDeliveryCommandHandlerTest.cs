using DeliverySystem.Api.CommandHandlers;
using DeliverySystem.Api.Commands;
using DeliverySystem.Domain.Deliveries;
using DeliverySystem.Infrastructure;
using DeliverySystem.Tools.Security;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DeliverySystem.Tests.CommandHandlers
{
    public class CompleteDeliveryCommandHandlerTest
    {
        private readonly Mock<IDeliveryRepository> _deliveryRepository;
        private readonly Mock<IExistsDeliverySpecification> _existsDelivery;
        private readonly Mock<IUserContext> _userContext;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public CompleteDeliveryCommandHandlerTest()
        {
            _deliveryRepository = new Mock<IDeliveryRepository>();
            _existsDelivery = new Mock<IExistsDeliverySpecification>();
            _userContext = new Mock<IUserContext>();
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task Handle_Uses_GetAsync_From_IDeliveryRepository_To_Get_The_Delivery()
        {
            // Arrange
            Arrange();

            var sut = new CompleteDeliveryCommandHandler(
                _deliveryRepository.Object,
                _existsDelivery.Object,
                _userContext.Object,
                _unitOfWork.Object);
            // Act
            await sut.Handle(NewCompleteDeliveryCommand(), new CancellationToken());
            // Assert
            _deliveryRepository.Verify(r => r.GetAsync(It.IsAny<Expression<Func<Delivery, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Uses_EnforceRule_From_IExistsDeliverySpecification()
        {
            // Arrange
            Arrange();

            var sut = new CompleteDeliveryCommandHandler(
                _deliveryRepository.Object,
                _existsDelivery.Object,
                _userContext.Object,
                _unitOfWork.Object);
            // Act
            await sut.Handle(NewCompleteDeliveryCommand(), new CancellationToken());
            // Assert
            _existsDelivery.Verify(s => s.EnforceRule(It.IsAny<Delivery>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Uses_Update_From_IDeliveryRepository_With_Delivery_State_Is_Completed()
        {
            // Arrange
            Arrange();

            var sut = new CompleteDeliveryCommandHandler(
                _deliveryRepository.Object,
                _existsDelivery.Object,
                _userContext.Object,
                _unitOfWork.Object);
            // Act
            await sut.Handle(NewCompleteDeliveryCommand(), new CancellationToken());
            // Assert
            _deliveryRepository.Verify(r => r.Update(It.Is<Delivery>(d => d.State == DeliveryState.Completed)), Times.Once);
        }

        [Fact]
        public async Task Handle_Uses_SaveAllAsync_From_IUnitOfWork()
        {
            // Arrange
            Arrange();

            var sut = new CompleteDeliveryCommandHandler(
                _deliveryRepository.Object,
                _existsDelivery.Object,
                _userContext.Object,
                _unitOfWork.Object);
            // Act
            await sut.Handle(NewCompleteDeliveryCommand(), new CancellationToken());
            // Assert
            _unitOfWork.Verify(u => u.SaveAllAsync(), Times.Once);
        }

        private void Arrange()
        {
            _deliveryRepository.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Delivery, bool>>>()))
                .Returns(NewDelivery());

            _userContext.Setup(u => u.UserDetails).Returns(UserDetails.New(3, Role.UserConsumerMarket));
        }

        private Task<Delivery> NewDelivery()
        {
            return Task.FromResult(Delivery.New(
                new AccessWindow(DateTime.UtcNow, DateTime.UtcNow.AddHours(2)),
                new Recipient("name", "address", "email@domain.com", "+447000000000"),
                new Order("123456", "Ikea"),
                userId: 3,
                partnerId: 2,
                createdBy: 1));
        }

        private CompleteDeliveryCommand NewCompleteDeliveryCommand()
        {
            return new CompleteDeliveryCommand(1);
        }
    }
}
