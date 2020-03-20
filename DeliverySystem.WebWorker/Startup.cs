using DeliverySystem.Domain.Deliveries;
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
using System.Reflection;

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
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddSingleton<IEventBus, EventBus>();
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IDeliveryRepository, DeliveryRepository>();
            services.AddHostedService<DeliveriesTimedHostedService>();

            services.AddDbContext<AppDbContext>(option =>
            {
                option = new DbContextOptionsBuilder<AppDbContext>()
                    .UseSqlServer(Configuration.GetConnectionString("AppEntities"));
            }, ServiceLifetime.Singleton);
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
    }
}
