using DeliverySystem.Api.CommandHandlers;
using DeliverySystem.Api.Commands;
using DeliverySystem.Api.Dtos;
using DeliverySystem.Domain.Deliveries;
using DeliverySystem.Infrastructure;
using DeliverySystem.Tools.Security;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DeliverySystem.Tests.CommandHandlers
{
    public class CreateDeliveryCommandHandlerTest
    {
        private readonly Mock<IDeliveryRepository> _deliveryRepository;
        private readonly Mock<IUserContext> _userContext;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public CreateDeliveryCommandHandlerTest()
        {
            _deliveryRepository = new Mock<IDeliveryRepository>();
            _userContext = new Mock<IUserContext>();
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task Handle_Uses_Add_From_IDeliveryRepository_To_Add_A_New_Delivery_Into_The_Context()
        {
            // Arrange
            _userContext.Setup(u => u.UserDetails).Returns(UserDetails.New(3, Role.UserConsumerMarket));

            var sut = new CreateDeliveryCommandHandler(
                _deliveryRepository.Object,
                _userContext.Object,
                _unitOfWork.Object);
            // Act
            await sut.Handle(NewCreativeDeliveryCommand(), new CancellationToken());
            // Assert
            _deliveryRepository.Verify(r => r.Add(It.IsAny<Delivery>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Uses_SaveAllAsync_From_IUnitOfWork()
        {
            // Arrange
            _userContext.Setup(u => u.UserDetails).Returns(UserDetails.New(3, Role.UserConsumerMarket));

            var sut = new CreateDeliveryCommandHandler(
                _deliveryRepository.Object,
                _userContext.Object,
                _unitOfWork.Object);
            // Act
            await sut.Handle(NewCreativeDeliveryCommand(), new CancellationToken());
            // Assert
            _unitOfWork.Verify(u => u.SaveAllAsync(), Times.Once);
        }

        private CreateDeliveryCommand NewCreativeDeliveryCommand()
        {
            return new CreateDeliveryCommand
            {
                AccessWindow = new AccessWindowDto
                {
                    StartTime = DateTime.UtcNow,
                    EndTime = DateTime.UtcNow.AddHours(2)
                },
                Order = new OrderDto
                {
                    OrderNumber = "123456",
                    Sender = "Ikea"
                },
                Recipient = new RecipientDto
                {
                    Address = "address",
                    Email = "email",
                    Name = "name",
                    PhoneNumber = "+447000000000"
                },
                PartnerId = 2,
                UserId = 3
            };
        }
    }
}
