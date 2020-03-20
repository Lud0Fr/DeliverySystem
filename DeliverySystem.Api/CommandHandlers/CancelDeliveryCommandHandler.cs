using DeliverySystem.Api.Commands;
using DeliverySystem.Domain.Deliveries;
using DeliverySystem.Infrastructure;
using DeliverySystem.Tools.Security;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DeliverySystem.Api.CommandHandlers
{
    public class CancelDeliveryCommandHandler : IRequestHandler<CancelDeliveryCommand>
    {
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly IExistsDeliverySpecification _existsDelivery;
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;

        public CancelDeliveryCommandHandler(
            IDeliveryRepository deliveryRepository,
            IExistsDeliverySpecification existsDelivery,
            IUserContext userContext,
            IUnitOfWork unitOfWork)
        {
            _deliveryRepository = deliveryRepository;
            _existsDelivery = existsDelivery;
            _userContext = userContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(
            CancelDeliveryCommand request,
            CancellationToken cancellationToken)
        {
            var delivery = await GetDelivery(request.DeliveryId);

            delivery.Cancel(_userContext.UserDetails.Id);

            _deliveryRepository.Update(delivery);
            await _unitOfWork.SaveAllAsync();

            return Unit.Value;
        }

        private async Task<Delivery> GetDelivery(int deliveryId)
        {
            var delivery = await _deliveryRepository.GetAsync(d =>
                d.Id == deliveryId &&
                _userContext.UserDetails.Role == Role.Partner
                    ? d.PartnerId == _userContext.UserDetails.Id
                    : d.UserId == _userContext.UserDetails.Id &&
                (d.State == DeliveryState.Created ||
                d.State == DeliveryState.Approved));

            _existsDelivery.EnforceRule(delivery, $"Delivery with id {deliveryId} not found");
            return delivery;
        }
    }
}
