using DeliverySystem.Domain.Deliveries;
using DeliverySystem.Domain.Subscribers;
using DeliverySystem.Infrastructure;
using DeliverySystem.Infrastructure.Repositories;
using DeliverySystem.Tools.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DeliverySystem.Tests
{
    public class TestBase
    {
        private IConfiguration _config;

        public IConfiguration Configuration
        {
            get
            {
                if (_config == null)
                {
                    var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", optional: false);
                    _config = builder.Build();
                }

                return _config;
            }
        }

        public IServiceProvider ServiceProvider { get; }

        public TestBase()
        {
            var services = new ServiceCollection();
            ServiceProvider = services.BuildServiceProvider();

            services.AddSingleton(Configuration);
        }
    }
}
