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
                userId: 123,
                partnerId: 456));
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
                userId: 123,
                partnerId: 456));
            //Assert
            Assert.False(validation.IsValid);
            Assert.Equal(1, validation.Errors.Count); 
            Assert.Equal("AccessWindow", validation.Errors[0].PropertyName);
            Assert.Equal("NotNullValidator", validation.Errors[0].ErrorCode);
        }

        [Fact]
        public void Validate_Returns_1_Error_When_Access_Window_StartTime_Is_Lower_Than_Current_Date()
        {
            // Arrange
            var sut = new CreateDeliveryCommandValidator();
            // Act
            var validation = sut.Validate(NewDeliveryCommand(
                new AccessWindowDto
                {
                    StartTime = DateTime.UtcNow.AddHours(-1),
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
                userId: 123,
                partnerId: 456));
            //Assert
            Assert.False(validation.IsValid);
            Assert.Equal(1, validation.Errors.Count);
            Assert.Equal("AccessWindow.StartTime", validation.Errors[0].PropertyName);
            Assert.Equal("GreaterThanValidator", validation.Errors[0].ErrorCode);
        }

        [Fact]
        public void Validate_Returns_1_Error_When_Access_Window_EndTime_Is_Lower_Than_StartTime()
        {
            // Arrange
            var sut = new CreateDeliveryCommandValidator();
            // Act
            var validation = sut.Validate(NewDeliveryCommand(
                new AccessWindowDto
                {
                    StartTime = DateTime.UtcNow,
                    EndTime = DateTime.UtcNow.AddHours(-5)
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
                userId: 123,
                partnerId: 456));
            //Assert
            Assert.False(validation.IsValid);
            Assert.Equal(1, validation.Errors.Count);
            Assert.Equal("AccessWindow.EndTime", validation.Errors[0].PropertyName);
            Assert.Equal("GreaterThanValidator", validation.Errors[0].ErrorCode);
        }

        [Fact]
        public void Validate_Returns_1_Error_When_Recipient_Is_Null()
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
                null,
                userId: 123,
                partnerId: 456));
            //Assert
            Assert.False(validation.IsValid);
            Assert.Equal(1, validation.Errors.Count);
            Assert.Equal("Recipient", validation.Errors[0].PropertyName);
            Assert.Equal("NotNullValidator", validation.Errors[0].ErrorCode);
        }

        [Fact]
        public void Validate_Returns_1_Error_When_Recipient_Name_Is_Empty()
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
                    Name = "",
                    PhoneNumber = "+447000000000"
                },
                userId: 123,
                partnerId: 456));
            //Assert
            Assert.False(validation.IsValid);
            Assert.Equal(1, validation.Errors.Count);
            Assert.Equal("Recipient.Name", validation.Errors[0].PropertyName);
            Assert.Equal("NotEmptyValidator", validation.Errors[0].ErrorCode);
        }

        [Fact]
        public void Validate_Returns_1_Error_When_Recipient_Name_Length_Is_Greater_Than_Expected()
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
                    Name = "namenamenamenamenamenamenamenamenamenamenamenamenamenamenamenamenamenamenamenamenamenamenamenamenamenamenamenamenamename",
                    PhoneNumber = "+447000000000"
                },
                userId: 123,
                partnerId: 456));
            //Assert
            Assert.False(validation.IsValid);
            Assert.Equal(1, validation.Errors.Count);
            Assert.Equal("Recipient.Name", validation.Errors[0].PropertyName);
            Assert.Equal("MaximumLengthValidator", validation.Errors[0].ErrorCode);
        }

        [Fact]
        public void Validate_Returns_1_Error_When_Recipient_Address_Is_Empty()
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
                    Address = "",
                    Email = "email@domain.com",
                    Name = "name",
                    PhoneNumber = "+447000000000"
                },
                userId: 123,
                partnerId: 456));
            //Assert
            Assert.False(validation.IsValid);
            Assert.Equal(1, validation.Errors.Count);
            Assert.Equal("Recipient.Address", validation.Errors[0].PropertyName);
            Assert.Equal("NotEmptyValidator", validation.Errors[0].ErrorCode);
        }

        [Fact]
        public void Validate_Returns_1_Error_When_Recipient_Email_Is_Empty()
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
                    Email = "",
                    Name = "name",
                    PhoneNumber = "+447000000000"
                },
                userId: 123,
                partnerId: 456));
            //Assert
            Assert.False(validation.IsValid);
            Assert.Equal(1, validation.Errors.Count);
            Assert.Equal("Recipient.Email", validation.Errors[0].PropertyName);
            Assert.Equal("NotEmptyValidator", validation.Errors[0].ErrorCode);
        }

        [Fact]
        public void Validate_Returns_1_Error_When_Recipient_Email_Is_Not_Valid()
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
                    Email = "emaildomaincom",
                    Name = "name",
                    PhoneNumber = "+447000000000"
                },
                userId: 123,
                partnerId: 456));
            //Assert
            Assert.False(validation.IsValid);
            Assert.Equal(1, validation.Errors.Count);
            Assert.Equal("Recipient.Email", validation.Errors[0].PropertyName);
            Assert.Equal("EmailValidator", validation.Errors[0].ErrorCode);
        }

        [Fact]
        public void Validate_Returns_1_Error_When_Recipient_PhoneNumber_Is_Empty()
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
                    PhoneNumber = ""
                },
                userId: 123,
                partnerId: 456));
            //Assert
            Assert.False(validation.IsValid);
            Assert.Equal(1, validation.Errors.Count);
            Assert.Equal("Recipient.PhoneNumber", validation.Errors[0].PropertyName);
            Assert.Equal("NotEmptyValidator", validation.Errors[0].ErrorCode);
        }

        [Fact]
        public void Validate_Returns_1_Error_When_Recipient_PhoneNumber_Is_Not_Valid()
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
                    PhoneNumber = "47000000"
                },
                userId: 123,
                partnerId: 456));
            //Assert
            Assert.False(validation.IsValid);
            Assert.Equal(1, validation.Errors.Count);
            Assert.Equal("Recipient.PhoneNumber", validation.Errors[0].PropertyName);
            Assert.Equal("PhoneNumberValidator", validation.Errors[0].ErrorCode);
        }

        [Fact]
        public void Validate_Returns_1_Error_When_Order_Is_Null()
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
                null,
                new RecipientDto
                {
                    Address = "address",
                    Email = "email@domain.com",
                    Name = "name",
                    PhoneNumber = "+447000000000"
                },
                userId: 123,
                partnerId: 456));
            //Assert
            Assert.False(validation.IsValid);
            Assert.Equal(1, validation.Errors.Count);
            Assert.Equal("Order", validation.Errors[0].PropertyName);
            Assert.Equal("NotNullValidator", validation.Errors[0].ErrorCode);
        }

        [Fact]
        public void Validate_Returns_1_Error_When_Order_OrderNumber_Is_Empty()
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
                    OrderNumber = "",
                    Sender = "Ikea",
                },
                new RecipientDto
                {
                    Address = "address",
                    Email = "email@domain.com",
                    Name = "name",
                    PhoneNumber = "+447000000000"
                },
                userId: 123,
                partnerId: 456));
            //Assert
            Assert.False(validation.IsValid);
            Assert.Equal(1, validation.Errors.Count);
            Assert.Equal("Order.OrderNumber", validation.Errors[0].PropertyName);
            Assert.Equal("NotEmptyValidator", validation.Errors[0].ErrorCode);
        }

        [Fact]
        public void Validate_Returns_1_Error_When_Order_Sender_Is_Empty()
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
                    Sender = "",
                },
                new RecipientDto
                {
                    Address = "address",
                    Email = "email@domain.com",
                    Name = "name",
                    PhoneNumber = "+447000000000"
                },
                userId: 123,
                partnerId: 456));
            //Assert
            Assert.False(validation.IsValid);
            Assert.Equal(1, validation.Errors.Count);
            Assert.Equal("Order.Sender", validation.Errors[0].PropertyName);
            Assert.Equal("NotEmptyValidator", validation.Errors[0].ErrorCode);
        }

        [Fact]
        public void Validate_Returns_1_Error_When_Order_Sender_Length_Is_Greater_Than_Expected()
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
                    Sender = "sendersendersendersendersendersendersendersendersendersendersendersendersendersendersendersendersendersender",
                },
                new RecipientDto
                {
                    Address = "address",
                    Email = "email@domain.com",
                    Name = "name",
                    PhoneNumber = "+447000000000"
                },
                userId: 123,
                partnerId: 456));
            //Assert
            Assert.False(validation.IsValid);
            Assert.Equal(1, validation.Errors.Count);
            Assert.Equal("Order.Sender", validation.Errors[0].PropertyName);
            Assert.Equal("MaximumLengthValidator", validation.Errors[0].ErrorCode);
        }

        [Fact]
        public void Validate_Returns_1_Error_When_UserId_Is_0()
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
                    Sender = "sender",
                },
                new RecipientDto
                {
                    Address = "address",
                    Email = "email@domain.com",
                    Name = "name",
                    PhoneNumber = "+447000000000"
                },
                userId: 0,
                partnerId: 456));
            //Assert
            Assert.False(validation.IsValid);
            Assert.Equal(1, validation.Errors.Count);
            Assert.Equal("UserId", validation.Errors[0].PropertyName);
            Assert.Equal("GreaterThanValidator", validation.Errors[0].ErrorCode);
        }

        [Fact]
        public void Validate_Returns_1_Error_When_PartnerId_Is_0()
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
                    Sender = "sender",
                },
                new RecipientDto
                {
                    Address = "address",
                    Email = "email@domain.com",
                    Name = "name",
                    PhoneNumber = "+447000000000"
                },
                userId: 123,
                partnerId: 0));
            //Assert
            Assert.False(validation.IsValid);
            Assert.Equal(1, validation.Errors.Count);
            Assert.Equal("PartnerId", validation.Errors[0].PropertyName);
            Assert.Equal("GreaterThanValidator", validation.Errors[0].ErrorCode);
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
