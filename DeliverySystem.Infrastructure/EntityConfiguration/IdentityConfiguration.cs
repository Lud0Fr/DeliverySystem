using DeliverySystem.Domain.Identities;
using DeliverySystem.Tools.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace DeliverySystem.Infrastructure.EntityConfiguration
{
    public class IdentityConfiguration : EntityTypeConfigurationBase<Identity>
    {
        protected override void ConfigureMore(EntityTypeBuilder<Identity> builder)
        {
            builder.ToTable("Identities");

            builder.HasData(DefaultIdentities());
        }

        private IEnumerable<Identity> DefaultIdentities()
        {
            yield return Identity.New("admin@admin.com", "password", Role.Admin).WithId(1);
            yield return Identity.New("partner@partner.com", "password", Role.Partner, null, 222).WithId(2);
            yield return Identity.New("user@user.com", "password", Role.UserConsumerMarket, 333, null).WithId(3);
        }
    }
}
