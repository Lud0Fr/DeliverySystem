using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using DeliverySystem.Api.CommandHandlers;
using DeliverySystem.Api.Commands;
using DeliverySystem.Domain.Identities;
using DeliverySystem.Domain.Identities.Services;
using Moq;
using Xunit;

namespace DeliverySystem.Tests.CommandHandlers
{
    public class SignInCommandHandlerTest
    {
        private readonly Mock<IIdentityService> _identityService;
        private readonly Mock<IIdentityRepository> _identityRepository;
        private readonly Mock<IExistsIdentitySpecification> _existsIdentity;

        public SignInCommandHandlerTest()
        {
            _identityService = new Mock<IIdentityService>();
            _identityRepository = new Mock<IIdentityRepository>();
            _existsIdentity = new Mock<IExistsIdentitySpecification>();
        }

        [Fact]
        public async Task Handle_Uses_GetAsync_From_IIdentityRepository_To_Retrieve_The_Identity()
        {
            // Arrange
            var sut = new SignInCommandHandler(_identityService.Object, _identityRepository.Object, _existsIdentity.Object);
            // Act
            await sut.Handle(NewSignInCommand(), new CancellationToken());
            // Asset
            _identityRepository.Verify(r => r.GetAsync(It.IsAny<Expression<Func<Identity, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Uses_EnforceRule_From_IExistsIdentitySpecification_To_Verify_That_The_Identity_Is_Not_Null()
        {
            // Arrange
            var sut = new SignInCommandHandler(_identityService.Object, _identityRepository.Object, _existsIdentity.Object);
            // Act
            await sut.Handle(NewSignInCommand(), new CancellationToken());
            // Asset
            _existsIdentity.Verify(s => s.EnforceRule(It.IsAny<Identity>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Uses_GenerateJWT_From_IIdentityService_To_Get_The_Jwt()
        {
            // Arrange
            var sut = new SignInCommandHandler(_identityService.Object, _identityRepository.Object, _existsIdentity.Object);
            // Act
            await sut.Handle(NewSignInCommand(), new CancellationToken());
            // Asset
            _identityService.Verify(s => s.GenerateJWT(It.IsAny<Identity>()), Times.Once);
        }

        private SignInCommand NewSignInCommand()
        {
            return new SignInCommand
            {
                Email = "email@domain.com",
                Password = "password"
            };
        }
    }
}
