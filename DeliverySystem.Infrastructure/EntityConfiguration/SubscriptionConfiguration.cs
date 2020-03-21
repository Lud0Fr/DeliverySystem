using DeliverySystem.Domain.Subscribers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace DeliverySystem.Infrastructure.EntityConfiguration
{
    public class SubscriptionConfiguration : EntityTypeConfigurationBase<Subscription>
    {
        protected override void ConfigureMore(EntityTypeBuilder<Subscription> builder)
        {
            builder.ToTable("Subscriptions");

            builder.HasData(DefaultSubscriptions());
        }

        private IEnumerable<Subscription> DefaultSubscriptions()
        {
            yield return new Subscription(1, EventType.DeliveryCreated).WithId(1);
            yield return new Subscription(1, EventType.DeliveryStateChanged).WithId(2);
        }
    }
}
