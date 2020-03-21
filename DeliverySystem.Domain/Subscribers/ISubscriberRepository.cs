using DeliverySystem.Tools.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverySystem.Domain.Subscribers
{
    public interface ISubscriberRepository : IRepository<Subscriber>
    {
        Task<IEnumerable<Subscriber>> GetAllByAsync(int partnerId);
    }
}
