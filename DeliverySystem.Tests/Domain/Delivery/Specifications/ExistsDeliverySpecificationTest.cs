using DeliverySystem.Domain.Deliveries;
using Moq;
using System;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace DeliverySystem.Tests.Domain.Identities.Specifications
{
    public class ExistsDeliverySpecificationTest
    {
        [Fact]
        public void EnforceRule_Throws_An_ValidationException_When_Delivery_Is_Null()
        {
            // Arrange
            var sut = new ExistsDeliverySpecification();
            // Assert
            Assert.Throws<ValidationException>(() => sut.EnforceRule(null, It.IsAny<string>()));
        }

        [Fact]
        public void EnforceRule_Does_Not_Throw_Any_Exception_When_Delivery_Is_Not_Null()
        {
            // Arrange
            var sut = new ExistsDeliverySpecification();
            // Act
            var exception = Record.Exception(() => sut.EnforceRule(NewDelivery(), It.IsAny<string>()));
            // Assert
            Assert.Null(exception);
        }

        private Delivery NewDelivery()
        {
            return Delivery.New(
                new AccessWindow(DateTime.UtcNow, DateTime.UtcNow.AddHours(2)),
                new Recipient("name", "address", "email@domain.com", "+447000000000"),
                new Order("123456", "Ikea"),
                3,
                2,
                1);
        }
    }
}
