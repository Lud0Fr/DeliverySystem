using DeliverySystem.Domain.Deliveries;

namespace DeliverySystem.Infrastructure.Repositories
{
    public class DeliveryRepository :
        RepositoryBase<Delivery, AppDbContext>,
        IDeliveryRepository
    {
        public DeliveryRepository(AppDbContext entities)
            : base(entities)
        { }
    }
}
