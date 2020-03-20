using DeliverySystem.Domain.Identities;
using DeliverySystem.Domain.Identities.Services;
using DeliverySystem.Tools;
using DeliverySystem.Tools.Security;
using Xunit;

namespace DeliverySystem.Tests.Domain.Identities.Services
{
    public class IdentityServiceTest : TestBase
    {
        [Fact]
        public void GenerateJwt_Returns_The_Jwt_With_The_Given_Identity()
        {
            // Arrange
            var identity = Identity.New("email@domain.com", "password", Role.Admin);
            var sut = new IdentityService(new JwtConfiguration(), Configuration);
            // Act
            var jwt = sut.GenerateJWT(identity);
            // Assert
            Assert.NotEmpty(jwt);
            Assert.Equal(3, jwt.Split('.').Length);
        }
    }
}
