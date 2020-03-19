using DeliverySystem.Domain.Identities;

namespace DeliverySystem.Infrastructure.Repositories
{
    public class IdentityRepository :
        RepositoryBase<Identity, AppDbContext>,
        IIdentityRepository
    {
        public IdentityRepository(AppDbContext entities)
            : base(entities)
        { }
    }
}
