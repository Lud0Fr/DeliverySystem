using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

        public TestBase()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton(Configuration);
        }
    }
}
