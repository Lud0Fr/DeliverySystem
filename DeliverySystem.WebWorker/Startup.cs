using Autofac;
using Autofac.Extensions.DependencyInjection;
using DeliverySystem.Domain.Deliveries;
using DeliverySystem.Domain.Subscribers;
using DeliverySystem.Infrastructure;
using DeliverySystem.Infrastructure.Repositories;
using DeliverySystem.Tools.Domain;
using DeliverySystem.WebWorker.TimedHostedServices;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

namespace DeliverySystem.WebWorker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Environment.SetEnvironmentVariable("BASEDIR", AppDomain.CurrentDomain.BaseDirectory);

            Configuration = configuration;

            // Configure serilog (logging)
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(configuration).CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddHostedService<DeliveriesTimedHostedService>();
            services.AddMediatR();

            services.AddDbContext<AppDbContext>(option =>
            {
                option = new DbContextOptionsBuilder<AppDbContext>()
                    .UseSqlServer(Configuration.GetConnectionString("AppEntities"));
            }, ServiceLifetime.Singleton);

            return ConfigureAutofac(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private AutofacServiceProvider ConfigureAutofac(IServiceCollection services)
        {
            services.AddAutofac();

            var builder = new ContainerBuilder();

            builder.RegisterType<EventBus>().As<IEventBus>();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<DeliveryRepository>().As<IDeliveryRepository>();
            builder.RegisterType<SubscriberRepository>().As<ISubscriberRepository>();
            builder.RegisterType<SubscriptionService>().As<ISubscriptionService>();
            builder.RegisterType<DeliveryEventHandler>().As<INotificationHandler<DeliveryCreatedEvent>>();
            builder.RegisterType<DeliveryEventHandler>().As<INotificationHandler<DeliveryStateChangedEvent>>();

            builder.Populate(services);

            var container = builder.Build();

            return new AutofacServiceProvider(container);
        }
    }
}
