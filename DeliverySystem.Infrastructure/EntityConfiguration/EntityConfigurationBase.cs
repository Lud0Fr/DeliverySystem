using DeliverySystem.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliverySystem.Infrastructure.EntityConfiguration
{
    public abstract class EntityTypeConfigurationBase<TEntity> :
        IEntityTypeConfiguration<TEntity> where TEntity : Entity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(col => col.Id);
            builder.Property(col => col.Id).ValueGeneratedOnAdd();
            builder.Property(col => col.CreatedAt).IsRequired(true).HasDefaultValueSql("getdate()");
            builder.Property(col => col.CreatedBy);
            builder.Property(col => col.UpdatedAt);
            builder.Property(col => col.UpdatedBy);

            ConfigureMore(builder);
        }

        protected abstract void ConfigureMore(EntityTypeBuilder<TEntity> builder);
    }
}
