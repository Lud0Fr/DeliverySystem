using DeliverySystem.Domain.Deliveries;
using DeliverySystem.Domain.Identities;
using DeliverySystem.Domain.Subscribers;
using DeliverySystem.Infrastructure.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DeliverySystem.Infrastructure
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Identity> Identities { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        public AppDbContext(
            DbContextOptions dbContextOptions,
            IConfiguration configuration)
            : base(dbContextOptions)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurations
            modelBuilder.ApplyConfiguration(new IdentityConfiguration());
            modelBuilder.ApplyConfiguration(new DeliveryConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriberConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriptionConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("AppEntities");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
