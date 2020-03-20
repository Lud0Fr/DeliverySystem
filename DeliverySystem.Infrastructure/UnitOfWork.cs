using DeliverySystem.Tools.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverySystem.Infrastructure
{
    #region Interface

    public interface IUnitOfWork
    {
        Task SaveAllAsync();
    }

    #endregion

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _entities;
        private readonly IEventBus _eventBus;

        public UnitOfWork(
            AppDbContext entities,
            IEventBus eventBus)
        {
            _entities = entities;
            _eventBus = eventBus;
        }

        public bool IsDisposed { get; private set; }

        public async Task SaveAllAsync()
        {
            using (var transaction = _entities.Database.BeginTransaction())
            {
                var entries = _entities.ChangeTracker.Entries<AggregateRoot>().ToList();

                foreach (var entry in entries)
                {
                    await _eventBus.Publish(entry.Entity.FlushEvents());
                }

                await _entities.SaveChangesAsync();
                transaction.Commit();
            }
        }

        public void Dispose()
        {
            if (IsDisposed) return;

            _entities.Dispose();

            GC.SuppressFinalize(this);

            IsDisposed = true;
        }
    }
}
