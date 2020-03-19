using AutoMapper;
using DeliverySystem.Api.Mapping;
using DeliverySystem.Domain.Deliveries;
using DeliverySystem.Domain.Identities;
using DeliverySystem.Domain.Identities.Services;
using DeliverySystem.Infrastructure;
using DeliverySystem.Infrastructure.Repositories;
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

namespace DeliverySystem
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
			services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMediatR(Assembly.GetExecutingAssembly());

            ConfigureAutoMapper(services);
            ConfigureDomainServices(services);
            ConfigureRepositories(services);
            ConfigureDbContext(services);
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

        private void ConfigureAutoMapper(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DeliveryMapping());
            });

            services.AddSingleton(mappingConfig.CreateMapper());
        }

        private void ConfigureDomainServices(IServiceCollection services)
        {
            services.AddScoped<IIdentityService, IdentityService>();
        }

        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IIdentityRepository, IdentityRepository>();
            services.AddScoped<IDeliveryRepository, DeliveryRepository>();
        }

        private void ConfigureDbContext(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(option =>
            {
                option = new DbContextOptionsBuilder<AppDbContext>()
                    .UseSqlServer(Configuration.GetConnectionString("AppEntities"));
            });
        }
    }
}
