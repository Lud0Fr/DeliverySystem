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
            accessWindow.Property(a => a.StartTime).HasColumnName("AccessWindowStart");
            accessWindow.Property(a => a.EndTime).HasColumnName("AccessWindowEnd");

            var recipient = builder.OwnsOne(d => d.Recipient);
            recipient.Property(r => r.Address).HasColumnName("RecipientAddress");
            recipient.Property(r => r.Email).HasColumnName("RecipientEmail").HasMaxLength(100);
            recipient.Property(r => r.Name).HasColumnName("RecipientName").HasMaxLength(150);
            recipient.Property(r => r.PhoneNumber).HasColumnName("RecipientPhoneNumber").HasMaxLength(15);

            var order = builder.OwnsOne(d => d.Order);
            order.Property(o => o.OrderNumber).HasColumnName("OrderNumber").HasMaxLength(20);
            order.Property(o => o.Sender).HasColumnName("Sender").HasMaxLength(200);
        }
    }
}
