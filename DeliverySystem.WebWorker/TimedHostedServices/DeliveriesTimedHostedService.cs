using DeliverySystem.Domain.Deliveries;
using DeliverySystem.Infrastructure;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("DeliverySystem.Tests")]
namespace DeliverySystem.WebWorker.TimedHostedServices
{
    public class DeliveriesTimedHostedService : TimedHostedServiceBase
    {
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeliveriesTimedHostedService(
            IConfiguration configuration,
            IDeliveryRepository deliveryRepository,
            IUnitOfWork unitOfWork)
            : base(configuration, "DeliveriesTimedHostedService")
        {
            _deliveryRepository = deliveryRepository;
            _unitOfWork = unitOfWork;
        }

        protected internal override async Task ProcessAsync(object state)
        {
            var count = Interlocked.Increment(ref executionCount);

            var expiredDeliveries = await _deliveryRepository.GetAllAsync(d =>
                (d.State == DeliveryState.Created || d.State == DeliveryState.Approved) &&
                d.AccessWindow.EndTime <= DateTime.UtcNow);

            foreach (var expiredDelivery in expiredDeliveries)
            {
                expiredDelivery.Expire();
                _deliveryRepository.Update(expiredDelivery);
            }

            await _unitOfWork.SaveAllAsync();

            Log.Information("DeliveriesTimedHostedService is working. Count: " + count);
        }
    }
}
