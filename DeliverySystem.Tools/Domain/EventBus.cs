using MediatR;
using System.Threading.Tasks;

namespace DeliverySystem.Tools.Domain
{
    #region Interface

    public interface IEventBus
    {
        Task Publish<TEvent>(params TEvent[] events) where TEvent : INotification;
    }

    #endregion

    public class EventBus : IEventBus
    {
        private readonly IMediator _mediator;

        public EventBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Publish<T>(params T[] events) where T : INotification
        {
            foreach (var @event in events)
            {
                await _mediator.Publish(@event);
            }
        }
    }
}
