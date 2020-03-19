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

            var accessWindow = builder.OwnsOne(d => d.AccessWindow);
            accessWindow.Property(a => a.StartTime).HasColumnName("AccessWindow");
            accessWindow.Property(a => a.EndTime).HasColumnName("EndTime");

            var recipient = builder.OwnsOne(d => d.Recipient);
            recipient.Property(r => r.Address).HasColumnName("Address");
            recipient.Property(r => r.Email).HasColumnName("Email");
            recipient.Property(r => r.Name).HasColumnName("Name");
            recipient.Property(r => r.PhoneNumber).HasColumnName("PhoneNumber");

            var order = builder.OwnsOne(d => d.Order);
            order.Property(o => o.OrderNumber).HasColumnName("OrderNumber");
            order.Property(o => o.Sender).HasColumnName("Sender");
        }
    }
}
