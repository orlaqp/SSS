using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SSS.Domain.CQRS.Trade.Event.Events;
using SSS.Domain.Seedwork.Attribute;
using SSS.Infrastructure.Util;
using SSS.Infrastructure.Util.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SSS.Domain.CQRS.Trade.Event.Handlers
{
    [DIService(ServiceLifetime.Scoped, typeof(INotificationHandler<TradeAddEvent>))]
    public class TradeUpdateEventHandler : INotificationHandler<TradeUpdateEvent>
    {
        private static ILogger _logger;

        public TradeUpdateEventHandler()
        {
            _logger = (ILogger)HttpContextService.Current.RequestServices.GetService(typeof(ILogger<TradeUpdateEventHandler>));
        }

        public Task Handle(TradeUpdateEvent @event, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"TradeUpdateEventHandler {@event.ToJson()}");
            return Task.CompletedTask;
        }
    }
}
