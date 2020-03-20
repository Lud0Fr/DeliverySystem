using DeliverySystem.Domain.Identities;
using DeliverySystem.Tools.Security;
using Moq;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace DeliverySystem.Tests.Domain.Identities.Specifications
{
    public class ExistsIdentitySpecificationTest
    {
        [Fact]
        public void EnforceRule_Throws_An_ValidationException_When_Identity_Is_Null()
        {
            // Arrange
            var sut = new ExistsIdentitySpecification();
            // Asset
            Assert.Throws<ValidationException>(() => sut.EnforceRule(null, It.IsAny<string>()));
        }

        [Fact]
        public void EnforceRule_Does_Not_Throw_Any_Exception_When_Identity_Is_Not_Null()
        {
            // Arrange
            var sut = new ExistsIdentitySpecification();
            // Act
            var exception = Record.Exception(() => sut.EnforceRule(NewIdentity(), It.IsAny<string>()));
            // Asset
            Assert.Null(exception);
        }

        private Identity NewIdentity()
        {
            return Identity.New(
                "email@domain.com",
                "password",
                Role.Admin);
        }
    }
}
