using DeliverySystem.Api.Commands;
using DeliverySystem.Domain.Deliveries;
using DeliverySystem.Infrastructure;
using DeliverySystem.Tools.Security;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DeliverySystem.Api.CommandHandlers
{
    public class CreateDeliveryCommandHandler : IRequestHandler<CreateDeliveryCommand>
    {
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDeliveryCommandHandler(
            IDeliveryRepository deliveryRepository,
            IUserContext userContext,
            IUnitOfWork unitOfWork)
        {
            _deliveryRepository = deliveryRepository;
            _userContext = userContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(
            CreateDeliveryCommand request,
            CancellationToken cancellationToken)
        {
            _deliveryRepository.Add(Delivery.New(
                new AccessWindow(request.AccessWindow.StartTime, request.AccessWindow.EndTime),
                new Recipient(request.Recipient.Name, request.Recipient.Address, request.Recipient.Email, request.Recipient.PhoneNumber),
                new Order(request.Order.OrderNumber, request.Order.Sender),
                request.UserId,
                request.PartnerId,
                _userContext.UserDetails.Id));

            await _unitOfWork.SaveAllAsync();

            return Unit.Value;
        }
    }
}
