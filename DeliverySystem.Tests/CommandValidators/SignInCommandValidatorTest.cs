using DeliverySystem.Api.Commands;
using DeliverySystem.Api.CommandValidators;
using Xunit;

namespace DeliverySystem.Tests.CommandValidators
{
    public class SignInCommandValidatorTest
    {
        [Fact]
        public void Validate_Returns_0_Error_When_All_The_Conditions_Are_Met()
        {
            // Arrange
            var sut = new SignInCommandValidator();
            // Act
            var validation = sut.Validate(NewSignInCommand());
            //Assert
            Assert.True(validation.IsValid);
        }

        [Fact]
        public void Validate_Returns_1_Error_When_Email_Is_Null()
        {
            // Arrange
            var sut = new SignInCommandValidator();
            // Act
            var validation = sut.Validate(NewSignInCommand(null));
            //Assert
            Assert.False(validation.IsValid);
            Assert.Equal(1, validation.Errors.Count);
            Assert.Equal("Email", validation.Errors[0].PropertyName);
            Assert.Equal("NotEmptyValidator", validation.Errors[0].ErrorCode);
        }

        [Fact]
        public void Validate_Returns_1_Error_When_Email_Is_Empty()
        {
            // Arrange
            var sut = new SignInCommandValidator();
            // Act
            var validation = sut.Validate(NewSignInCommand(""));
            //Assert
            Assert.False(validation.IsValid);
            Assert.Equal(1, validation.Errors.Count);
            Assert.Equal("Email", validation.Errors[0].PropertyName);
            Assert.Equal("NotEmptyValidator", validation.Errors[0].ErrorCode);
        }

        [Fact]
        public void Validate_Returns_1_Error_When_Password_Is_Null()
        {
            // Arrange
            var sut = new SignInCommandValidator();
            // Act
            var validation = sut.Validate(NewSignInCommand("email@domain.com", null));
            //Assert
            Assert.False(validation.IsValid);
            Assert.Equal(1, validation.Errors.Count);
            Assert.Equal("Password", validation.Errors[0].PropertyName);
            Assert.Equal("NotEmptyValidator", validation.Errors[0].ErrorCode);
        }

        [Fact]
        public void Validate_Returns_1_Error_When_Password_Is_Empty()
        {
            // Arrange
            var sut = new SignInCommandValidator();
            // Act
            var validation = sut.Validate(NewSignInCommand("password", ""));
            //Assert
            Assert.False(validation.IsValid);
            Assert.Equal(1, validation.Errors.Count);
            Assert.Equal("Password", validation.Errors[0].PropertyName);
            Assert.Equal("NotEmptyValidator", validation.Errors[0].ErrorCode);
        }

        private SignInCommand NewSignInCommand(
            string email = "email@domain.com",
            string password = "password")
        {
            return new SignInCommand
            {
                Email = email,
                Password = password
            };
        }
    }
}
