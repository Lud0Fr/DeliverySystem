using System;
using DeliverySystem.Api.Commands;
using DeliverySystem.Api.CommandValidators;
using DeliverySystem.Api.Dtos;
using Xunit;

namespace DeliverySystem.Tests.CommandValidators
{
    public class CreateDeliveryCommandValidatorTest
    {
        [Fact]
        public void Validate_Returns_0_Error_When_All_The_Conditions_Are_Met()
        {
            // Arrange
            var sut = new CreateDeliveryCommandValidator();
            // Act
            var validation = sut.Validate(NewDeliveryCommand(
                new AccessWindowDto
                {
                    StartTime = DateTime.UtcNow,
                    EndTime = DateTime.UtcNow.AddHours(5)
                },
                new OrderDto
                {
                    OrderNumber = "123456",
                    Sender = "Ikea",
                },
                new RecipientDto
                {
                    Address = "address",
                    Email = "email@domain.com",
                    Name = "name",
                    PhoneNumber = "+447000000000"
                },
                123,
                456));
            //Assert
            Assert.True(validation.IsValid);
        }

        [Fact]
        public void Validate_Returns_1_Error_When_Access_Window_Is_Null()
        {
            // Arrange
            var sut = new CreateDeliveryCommandValidator();
            // Act
            var validation = sut.Validate(NewDeliveryCommand(
                null,
                new OrderDto
                {
                    OrderNumber = "123456",
                    Sender = "Ikea",
                },
                new RecipientDto
                {
                    Address = "address",
                    Email = "email@domain.com",
                    Name = "name",
                    PhoneNumber = "+447000000000"
                },
                123,
                456));
            //Assert
            Assert.False(validation.IsValid);
            Assert.Equal(1, validation.Errors.Count); 
            Assert.Equal("AccessWindow", validation.Errors[0].PropertyName);
            Assert.Equal("NotNullValidator", validation.Errors[0].ErrorCode);
        }

        private CreateDeliveryCommand NewDeliveryCommand(
            AccessWindowDto accessWindow,
            OrderDto order,
            RecipientDto recipient,
            int userId,
            int partnerId)
        {
            return new CreateDeliveryCommand
            {
                AccessWindow = accessWindow,
                Order = order,
                Recipient = recipient,
                PartnerId = partnerId,
                UserId = userId
            };
        }
    }
}
