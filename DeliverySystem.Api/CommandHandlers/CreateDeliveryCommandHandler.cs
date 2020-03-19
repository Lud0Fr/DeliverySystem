using DeliverySystem.Api.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DeliverySystem.Api.CommandHandlers
{
    public class CreateDeliveryCommandHandler : AsyncRequestHandler<CreateDeliveryCommand>
    {
        public CreateDeliveryCommandHandler()
        {
        }

        protected override async Task Handle(
            CreateDeliveryCommand request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
