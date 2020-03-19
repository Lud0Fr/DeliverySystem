using DeliverySystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DeliverySystem.Infrastructure.Repositories
{
    public interface IRepository<TAggregateRoot> where TAggregateRoot : AggregateRoot
    {
        Task<TAggregateRoot> GetAsync(Expression<Func<TAggregateRoot, bool>> predicate);
        Task<IEnumerable<TAggregateRoot>> GetAllAsync();
        Task<IEnumerable<TAggregateRoot>> GetAllAsync(Expression<Func<TAggregateRoot, bool>> predicate);
        void Add(TAggregateRoot aggregateToAdd);
        void Update(TAggregateRoot aggregateToUpdate);
        void Delete(TAggregateRoot aggregateToDelete);
    }
}
