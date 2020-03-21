using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliverySystem.Domain.Subscribers;
using Microsoft.EntityFrameworkCore;

namespace DeliverySystem.Infrastructure.Repositories
{
    public class SubscriberRepository :
        RepositoryBase<Subscriber, AppDbContext>,
        ISubscriberRepository
    {
        public SubscriberRepository(AppDbContext entities)
            : base(entities)
        { }

        public async Task<IEnumerable<Subscriber>> GetAllByAsync(int partnerId)
        {
            return await Query
                .Include(s => s.Subscriptions)
                .Where(s => s.PartnerId == partnerId)
                .ToListAsync();
        }
    }
}
