using DeliverySystem.Tools.Domain;
using DeliverySystem.Tools.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DeliverySystem.Infrastructure.Repositories
{
    public class RepositoryBase<T, C> : IRepository<T>
        where T : AggregateRoot
        where C : DbContext
    {
        protected readonly DbSet<T> _set;
        protected readonly DbContext _entities;

        protected DbSet<T> Query
        {
            get { return _set; }
            set { }
        }

        public RepositoryBase(C entities)
        {
            _entities = entities;
            _set = entities.Set<T>();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _set.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _set.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _set.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _set.AsNoTracking().Where(predicate).ToListAsync();
        }

        public void Add(T entityToAdd)
        {
            _set.Add(entityToAdd);
        }

        public void Update(T entityToUpdate)
        {
            _set.Update(entityToUpdate);
        }

        public void Delete(T entityToDelete)
        {
            _set.Remove(entityToDelete);
        }
    }
}
