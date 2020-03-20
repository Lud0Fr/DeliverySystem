using MediatR;
using System.Collections.Generic;

namespace DeliverySystem.Tools.Domain
{
    public class AggregateRoot : Entity
    {
        private readonly Queue<INotification> _events;

        protected AggregateRoot()
        {
            _events = new Queue<INotification>();
        }

        protected void ApplyEvent(INotification @event)
        {
            _events.Enqueue(@event);
        }

        public virtual INotification[] FlushEvents()
        {
            var events = _events.ToArray();

            _events.Clear();

            return events;
        }
    }
}
