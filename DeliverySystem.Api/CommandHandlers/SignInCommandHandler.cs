using DeliverySystem.Api.Commands;
using DeliverySystem.Domain.Identities;
using DeliverySystem.Domain.Identities.Services;
using DeliverySystem.Tools;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DeliverySystem.Api.CommandHandlers
{
    public class SignInCommandHandler : IRequestHandler<SignInCommand, string>
    {
        private readonly IIdentityService _identityService;
        private readonly IIdentityRepository _identityRepository;
        private readonly IExistsIdentitySpecification _existsIdentity;

        public SignInCommandHandler(
            IIdentityService identityService,
            IIdentityRepository identityRepository,
            IExistsIdentitySpecification existsIdentity)
        {
            _identityService = identityService;
            _identityRepository = identityRepository;
            _existsIdentity = existsIdentity;
        }

        public async Task<string> Handle(
            SignInCommand request,
            CancellationToken cancellationToken)
        {
            var identity = await GetIdentity(request);

            return _identityService.GenerateJWT(identity);
        }

        private async Task<Identity> GetIdentity(SignInCommand request)
        {
            var identity = await _identityRepository.GetAsync(i =>
                i.Email == request.Email &&
                i.PasswordHash == request.Password.ComputeSHA1());

            _existsIdentity.EnforceRule(identity, "Invalid email/password");
            return identity;
        }
    }
}
