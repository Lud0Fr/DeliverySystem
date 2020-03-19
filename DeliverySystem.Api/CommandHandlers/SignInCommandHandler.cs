using System.Threading;
using System.Threading.Tasks;
using DeliverySystem.Api.Commands;
using DeliverySystem.Domain.Identities;
using DeliverySystem.Domain.Identities.Services;
using MediatR;

namespace DeliverySystem.Api.CommandHandlers
{
    public class SignInCommandHandler : IRequestHandler<SignInCommand, string>
    {
        private readonly IIdentityService _identityService;
        private readonly IIdentityRepository _identityRepository;

        public SignInCommandHandler(
            IIdentityService identityService,
            IIdentityRepository identityRepository)
        {
            _identityService = identityService;
            _identityRepository = identityRepository;
        }

        public async Task<string> Handle(
            SignInCommand request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
