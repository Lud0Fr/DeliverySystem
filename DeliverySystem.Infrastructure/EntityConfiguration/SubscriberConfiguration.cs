using DeliverySystem.Domain.Subscribers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace DeliverySystem.Infrastructure.EntityConfiguration
{
    public class SubscriberConfiguration : EntityTypeConfigurationBase<Subscriber>
    {
        protected override void ConfigureMore(EntityTypeBuilder<Subscriber> builder)
        {
            builder.ToTable("Subscribers");

            builder
                .HasMany(s => s.Subscriptions)
                .WithOne()
                .HasForeignKey(s => s.SubscriberId);

            builder.HasData(DefaultSusbcribers());
        }

        private IEnumerable<Subscriber> DefaultSusbcribers()
        {
            yield return Subscriber.New(222, "https://partner222.com/webhooks").WithId(1);
        }
    }
}
