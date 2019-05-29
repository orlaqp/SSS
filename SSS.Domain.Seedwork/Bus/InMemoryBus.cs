using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SSS.Domain.Seedwork.Attribute;
using SSS.Domain.Seedwork.Events;
using System.Threading.Tasks;

namespace SSS.Domain.Seedwork.Bus
{
    [DIService(ServiceLifetime.Scoped, typeof(IMediatorHandler))]
    public class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task SendCommand<T>(T command) where T : Command.Command
        {
            return _mediator.Send(command);
        }

        public Task RaiseEvent<T>(T @event) where T : Event
        {
            return _mediator.Publish(@event);
        }
    }
}
