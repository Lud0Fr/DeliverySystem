using DeliverySystem.Domain.Deliveries;
using DeliverySystem.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DeliverySystem.WebWorker.TimedHostedServices
{
    public abstract class TimedHostedServiceBase : IHostedService, IDisposable
    {
        protected int executionCount = 0;
        protected Timer _timer;
        protected double _period;
        protected readonly IConfiguration _configuration;
        protected readonly IDeliveryRepository _deliveryRepository;
        protected readonly IUnitOfWork _unitOfWork;

        public TimedHostedServiceBase(
            IConfiguration configuration,
            IDeliveryRepository deliveryRepository,
            IUnitOfWork unitOfWork,
            string timedHostedServiceCOnfiguration)
        {
            _configuration = configuration;
            _deliveryRepository = deliveryRepository;
            _unitOfWork = unitOfWork;
            _period = Convert.ToDouble(_configuration.GetSection($"{timedHostedServiceCOnfiguration}:Period").Value);
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            Log.Information("DeliveriesTimedHostedService running.");

            _timer = new Timer(
                (state) => { ProcessAsync(state).GetAwaiter(); },
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(_period));

            return Task.CompletedTask;
        }

        protected internal abstract Task ProcessAsync(object state);

        public Task StopAsync(CancellationToken stoppingToken)
        {
            Log.Information("DeliveriesTimedHostedService is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
