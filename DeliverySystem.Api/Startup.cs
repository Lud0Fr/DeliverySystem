using AutoMapper;
using DeliverySystem.Api.Commands;
using DeliverySystem.Api.CommandValidators;
using DeliverySystem.Api.Mapping;
using DeliverySystem.Domain.Deliveries;
using DeliverySystem.Domain.Identities;
using DeliverySystem.Domain.Identities.Services;
using DeliverySystem.Infrastructure;
using DeliverySystem.Infrastructure.Repositories;
using DeliverySystem.Tools;
using DeliverySystem.Tools.Domain;
using DeliverySystem.Tools.Security;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Reflection;
using System.Security.Principal;
using System.Text;

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

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation();


            ConfigureAuthenticationAndAuthorization(services);
            ConfigureSecurity(services);
            ConfigureAutoMapper(services);
            ConfigureDomainEvents(services);
            ConfigureDomainServices(services);
            ConfigureValidators(services);
            ConfigureRepositories(services);
            ConfigureSpecifications(services);

            services.AddMediatR(Assembly.GetExecutingAssembly());

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

        private void ConfigureAuthenticationAndAuthorization(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtOptions =>
                {
                    JwtOptions.RequireHttpsMetadata = false;
                    JwtOptions.SaveToken = true;
                    JwtOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(Configuration.GetSection("Jwt:Key").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = true,
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(UserRolesRequirement.PolicyKey, policy => policy.Requirements.Add(new UserRolesRequirement()));
            });

            services.AddSingleton<IAuthorizationHandler, UserRolesRequirementHandler>();
        }

        private void ConfigureSecurity(IServiceCollection services)
        {
            var jwtConfig = new JwtConfiguration();
            Configuration.Bind("JWT", jwtConfig);
            services.AddSingleton(jwtConfig);

            services.AddSingleton<IUserContext, UserContext>();
        }

        private void ConfigureDomainEvents(IServiceCollection services)
        {
            services.AddScoped<IEventBus, EventBus>();
        }

        private void ConfigureDomainServices(IServiceCollection services)
        {
            services.AddScoped<IIdentityService, IdentityService>();
        }

        private void ConfigureValidators(IServiceCollection services)
        {
            services.AddScoped<IValidator<SignInCommand>, SignInCommandValidator>();
            services.AddScoped<IValidator<CreateDeliveryCommand>, CreateDeliveryCommandValidator>();
        }

        private void ConfigureSpecifications(IServiceCollection services)
        {
            services.AddScoped<IExistsIdentitySpecification, ExistsIdentitySpecification>();
            services.AddScoped<IExistsDeliverySpecification, ExistsDeliverySpecification>();
        }

        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
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
