using DeliverySystem.Domain.Deliveries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliverySystem.Infrastructure.EntityConfiguration
{
    public class DeliveryConfiguration : EntityTypeConfigurationBase<Delivery>
    {
        protected override void ConfigureMore(EntityTypeBuilder<Delivery> builder)
        {
            builder.ToTable("Deliveries");

            builder.OwnsOne(d => d.AccessWindow);
            builder.OwnsOne(d => d.Recipient);
            builder.OwnsOne(d => d.Order);
        }
    }
}
